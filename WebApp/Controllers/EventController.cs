using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
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
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.EventController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator, Roles.BackofficeEventUser, Roles.FrontSuperAdministrator, Roles.FrontEventAdministrator, Roles.FrontEventUser)]
    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly ITenantService tenantService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public EventController(IEventService eventService, ITenantService tenantService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.eventService = eventService;
            this.tenantService = tenantService;
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

            var events = this.eventService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<Event, bool>> predicate = null;

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
            Expression<Func<Event, object>> order = null;
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

            var response = new DataTableResponse<Event, EventRow>(request, events, (c =>
            {
                var row = new EventRow(this.localizer, this.operationContext)
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
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator, Roles.FrontSuperAdministrator, Roles.FrontEventAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Return view with empty object 
            return View("CreateOrEdit", new EventViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator, Roles.FrontSuperAdministrator, Roles.FrontEventAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Date,TenantId,Duration,FilesIds,Location")] EventViewModel eventViewModel)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                var eventEntity = eventViewModel.ToEntity();

                await this.eventService.CreateAsync(eventEntity);
                return RedirectToAction("Index", "Event");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            return View("CreateOrEdit", eventViewModel);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator, Roles.FrontSuperAdministrator, Roles.FrontEventAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get Event from database 
            var eventEntity = this.eventService.GetFull(new Guid(id));

            // Return view with Event info
            return View("CreateOrEdit", new EventViewModel(eventEntity));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator, Roles.FrontSuperAdministrator, Roles.FrontEventAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description,Date,Duration,FilesIds,Location,LocationId")] EventViewModel eventViewModel)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                var eventEntity = eventViewModel.ToEntity();

                await this.eventService.UpdateAsync(eventEntity);
                return RedirectToAction("Index", "Event");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            return View("CreateOrEdit", eventViewModel);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get Event from database 
            var eventEntity = this.eventService.GetFull(new Guid(id));

            // Return view with Event info
            return View(new EventViewModel(eventEntity));
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator, Roles.FrontSuperAdministrator, Roles.FrontEventAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.eventService.DeleteAsync(new Guid(id));
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