using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Attributes;
using WebApp.Models;
using WebApp.WebTools.DataTable;
using JsLang = Binit.Framework.Localization.LocalizationConstants.WebApp.Js;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.HolidayTypeController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator, Roles.BackofficeHolidayTypeUser, Roles.FrontSuperAdministrator, Roles.FrontHolidayTypeAdministrator, Roles.FrontHolidayTypeUser)]
    public class HolidayTypeController : Controller
    {
        private IService<HolidayType> holidayTypeService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public HolidayTypeController(IService<HolidayType> holidayTypeService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.holidayTypeService = holidayTypeService;
            this.localizer = localizer;
            this.operationContext = operationContext;
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

            var holidayTypes = this.holidayTypeService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<HolidayType, bool>> predicate = null;

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
            Expression<Func<HolidayType, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "description": order = o => o.Description; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<HolidayType, HolidayTypeRow>(request, holidayTypes, (c =>
            {
                var row = new HolidayTypeRow(this.localizer, this.operationContext)
                {
                    DT_RowId = c.Id.ToString(),
                    Name = c.Name,
                    Description = c.Description
                };

                row.SetActions(c);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayTypeAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";
            // Return view with empty object 
            return View("CreateOrEdit", new HolidayTypeViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayTypeAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description")] HolidayTypeViewModel holidayType)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.holidayTypeService.CreateAsync(holidayType.ToEntity());
                return RedirectToAction("Index", "HolidayType");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            return View("CreateOrEdit", holidayType);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayTypeAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get holidayType from database 
            var holidayType = this.holidayTypeService.Get(new Guid(id));

            // Return view with holidayType info
            return View("CreateOrEdit", new HolidayTypeViewModel(holidayType));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayTypeAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description")] HolidayTypeViewModel holidayType)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.holidayTypeService.UpdateAsync(holidayType.ToEntity());
                return RedirectToAction("Index", "HolidayType");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            return View("CreateOrEdit", holidayType);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get holidayType from database 
            var holidayType = this.holidayTypeService.Get(new Guid(id));

            // Return view with holidayType info
            return View(new HolidayTypeViewModel(holidayType));
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator, Roles.FrontSuperAdministrator, Roles.FrontHolidayTypeAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.holidayTypeService.DeleteAsync(new Guid(id));
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
    }
}