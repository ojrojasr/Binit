using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.WebTools.DataTable;
using JsLang = Binit.Framework.Localization.LocalizationConstants.WebApp.Js;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.TenantController;

namespace WebApp.Controllers
{
    [Authorize(Roles = Roles.BackofficeSuperAdministrator)]
    public class TenantController : Controller
    {
        private ITenantService tenantService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public TenantController(ITenantService tenantService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
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

            var tenants = this.tenantService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<Tenant, bool>> predicate = null;

            // If search term is not empty...
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = p =>
                (
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm)
                );
            }

            // Sort
            Expression<Func<Tenant, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "code": order = o => o.Code; break;
                    case "name": order = o => o.Name; break;
                    default: order = o => o.CreatedDate; break;
                }
            };


            var response = new DataTableResponse<Tenant, TenantRow>(request, tenants, (t =>
            {
                var row = new TenantRow(this.localizer)
                {
                    DT_RowId = t.Id.ToString(),
                    Code = t.Code,
                    Name = t.Name
                };

                row.SetActions(t);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";
            // Return view with empty object 
            return View("CreateOrEdit", new TenantViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Code,ParentId")] TenantViewModel Tenant)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.tenantService.CreateAsync(Tenant.ToEntity());
                return RedirectToAction("Index", "Tenant");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            return View("CreateOrEdit", Tenant);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get Tenant from database 
            var Tenant = this.tenantService.Get(new Guid(id));

            // Return view with Tenant info
            return View("CreateOrEdit", new TenantViewModel(Tenant));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Path,Name,Description,Code,ParentId")] TenantViewModel Tenant)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.tenantService.UpdateAsync(Tenant.ToEntity());
                return RedirectToAction("Index", "Tenant");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            return View("CreateOrEdit", Tenant);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get Tenant from database 
            var Tenant = this.tenantService.Get(new Guid(id));

            // Return view with Tenant info
            return View(new TenantViewModel(Tenant));
        }
        #endregion

        #region Delete
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.tenantService.DeleteAsync(new Guid(id));
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