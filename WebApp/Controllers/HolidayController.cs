using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Helpers.Excel;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using WebApp.Attributes;
using WebApp.Models;
using WebApp.WebTools.DataTable;
using JsLang = Binit.Framework.Localization.LocalizationConstants.WebApp.Js;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.HolidayController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator, Roles.BackofficeHolidayUser, Roles.FrontSuperAdministrator, Roles.FrontHolidayAdministrator, Roles.FrontHolidayUser)]
    public class HolidayController : Controller
    {
        private readonly IHolidayService holidayService;
        private readonly IService<HolidayType> holidayTypeService;
        private readonly IUserService userService;
        private readonly IHolidayBusinessLogic holidayBusinessLogic;

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;
        private readonly IConfiguration configuration;

        public HolidayController(IHolidayService holidayService, IService<HolidayType> holidayTypeService,
            IUserService userService, IHolidayBusinessLogic holidayBusinessLogic, IStringLocalizer<SharedResources> localizer,
            IOperationContext operationContext, IConfiguration configuration)
        {
            this.holidayService = holidayService;
            this.holidayTypeService = holidayTypeService;
            this.userService = userService;
            this.holidayBusinessLogic = holidayBusinessLogic;
            this.localizer = localizer;
            this.operationContext = operationContext;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = localizer[Lang.IndexTitle];
            // This is required in order to localize datatables.
            ViewData["DatatableResources"] = JsLocalizer.GetLocalizedResources(JsLang.Datatables, this.localizer);
            return View();
        }

        #region Get
        [HttpPost]
        public JsonResult GetAll(DataTableRequest request)
        {
            var holidays = this.holidayService.GetFull();
            var searchTerm = request.search.value;
            Expression<Func<Holiday, bool>> predicate = null;

            // If search term is not empty...
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = p =>
                (
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm)
                );
            }

            // Order
            Expression<Func<Holiday, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "description": order = o => o.Description; break;
                    case "reason": order = o => o.Reason.Name; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<Holiday, HolidayRow>(request, holidays, (h =>
            {
                var row = new HolidayRow(this.localizer, this.operationContext)
                {
                    DT_RowId = h.Id.ToString(),
                    Name = h.Name,
                    Description = h.Description,
                    Reason = h.Reason.Name
                };

                if (h.Users.Any())
                {
                    var users = h.Users.Select(u => u.User.Name + " " + u.User.LastName).ToList();
                    row.Users = new DataTableMultivalueColumn(users)
                    {
                        DefaultStyle = DataTableTagStyle.Blue,
                        Limit = 4
                    };
                }

                row.SetActions(h);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get reasons
            ViewData["Reasons"] = this.GetReasons();

            // Return view with empty object 
            return View("CreateOrEdit", new HolidayViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Message,StartDate,EndDate,ReasonId,Users,UsersIds")] HolidayViewModel holiday)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.holidayService.CreateAsync(holiday.ToEntity());
                return RedirectToAction("Index", "Holiday");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get reasons
            if (holiday.ReasonId != null)
            {
                ViewData["Reasons"] = this.GetReasons(new Guid(holiday.ReasonId));
            }
            else
            {
                ViewData["Reasons"] = this.GetReasons();
            }

            // Get selected users from db
            if (holiday.UsersIds != null && holiday.UsersIds.Count > 0)
            {
                holiday.Users = new List<SelectListItem>();
                var users = await this.userService.GetMany(holiday.UsersIds);
                foreach (var item in users)
                {
                    holiday.Users.Add(new SelectListItem(item.Email, item.Id.ToString(), true));
                }
            }

            return View("CreateOrEdit", holiday);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get holiday from database 
            var holiday = this.holidayService.GetFull(new Guid(id));

            // Get reasons
            ViewData["Reasons"] = this.GetReasons(holiday.ReasonId);

            // Return view with holiday info
            return View("CreateOrEdit", new HolidayViewModel(holiday));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description,Message,StartDate,EndDate,ReasonId,Users,UsersIds")] HolidayViewModel holiday)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.holidayService.UpdateAsync(holiday.ToEntity());
                return RedirectToAction("Index", "Holiday");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get reasons
            ViewData["Reasons"] = this.GetReasons(new Guid(holiday.ReasonId));

            // Get selected users from db
            if (holiday.UsersIds != null && holiday.UsersIds.Count > 0)
            {
                holiday.Users = new List<SelectListItem>();
                var users = await this.userService.GetMany(holiday.UsersIds);
                foreach (var item in users)
                {
                    holiday.Users.Add(new SelectListItem(item.Email, item.Id.ToString(), true));
                }
            }

            return View("CreateOrEdit", holiday);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get holiday from database 
            var holiday = this.holidayService.GetFull(new Guid(id));

            // Return view with holiday info
            return View(new HolidayViewModel(holiday));
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.holidayService.DeleteAsync(new Guid(id));
                message = localizer[Lang.DeleteSuccess];
            }
            catch (NotFoundException ex)
            {
                this.HttpContext.Response.StatusCode = 404;
                message = ex.Message;
            }
            catch (UserException ex)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = ex.Message;
            }
            catch (Exception)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = localizer[Lang.DeleteUnexpectedError];
            }

            return new JsonResult(new
            {
                message = message
            });
        }
        #endregion

        #region Import Excel
        /// <summary>
        /// Imports a list of holidays from an excel file.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> ImportExcel()
        {
            // Extract file from request.
            var formFile = this.Request.Form.Files.FirstOrDefault();

            var message = string.Empty;
            try
            {
                await this.holidayBusinessLogic.ImportExcel(formFile);
            }
            catch (ValidationException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = ex.Message;
            }
            catch (NotFoundException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                message = ex.Message;
            }
            catch (UserException ex)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = ex.Message;
            }
            catch (Exception)
            {
                this.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                message = localizer[Lang.ImportUnexpectedError];
            }

            return new JsonResult(new
            {
                message = message
            });
        }
        #endregion

        #region Export Excel

        /// <summary>
        /// Exports a filtered list of holidays into an excel file.
        /// Uses the same filter as DataTable.
        /// </summary>
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayAdministrator)]
        public async Task<FileResult> ExportExcel(string searchTerm)
        {
            ExportResult exportResult = await this.holidayBusinessLogic.ExportExcel(searchTerm);

            return File(exportResult.Stream, exportResult.ExportMimeType, exportResult.Filename);
        }

        #endregion

        private List<SelectListItem> GetReasons(Guid? selectedReason = null)
        {
            if (selectedReason == null)
            {
                return this.holidayTypeService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            }
            else
            {
                return this.holidayTypeService.GetAll()
                       .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == selectedReason)).ToList();
            }
        }

        [HttpGet]
        public async Task<JsonResult> SearchUsers(string term)
        {
            var users = await this.userService.SearchUsersAsync(term);

            return new JsonResult(new
            {
                results =
                users.Select(u => new
                {
                    id = u.Id.ToString(),
                    text = u.Email
                })
            });
        }
    }
}