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
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.GeneroController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator)]
    public class GeneroController : Controller
    {
        private IService<Genero> generoService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public GeneroController(IService<Genero> generoService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.generoService = generoService;
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

            var categories = this.generoService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<Genero, bool>> predicate = null;

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
            Expression<Func<Genero, object>> order = null;
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

            var response = new DataTableResponse<Genero, GeneroRow>(request, categories, (c =>
            {
                var row = new GeneroRow(this.localizer, this.operationContext)
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
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";
            // Return view with empty object 
            return View("CreateOrEdit", new GeneroViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create(GeneroViewModel genero)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.generoService.CreateAsync(genero.ToEntity());
                return RedirectToAction("Index", "Genero");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            return View("CreateOrEdit", genero);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get genero from database 
            var genero = this.generoService.Get(new Guid(id));

            // Return view with genero info
            return View("CreateOrEdit", new GeneroViewModel(genero));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit(GeneroViewModel genero)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.generoService.UpdateAsync(genero.ToEntity());
                return RedirectToAction("Index", "Genero");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            return View("CreateOrEdit", genero);
        }
        #endregion

        #region Details
        //public IActionResult Details(string id)
        //{
        //    ViewData["Title"] = localizer[Lang.DetailsTitle];

        //    // Get genero from database 
        //    var genero = this.generoService.Get(new Guid(id));

        //    // Return view with genero info
        //    return View(new GeneroViewModel(genero));
        //}
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.generoService.DeleteAsync(new Guid(id));
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