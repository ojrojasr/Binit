using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Attributes;
using WebApp.Models;
using WebApp.WebTools.DataTable;
using JsLang = Binit.Framework.Localization.LocalizationConstants.WebApp.Js;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.ProductController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.BackofficeProductUser, Roles.FrontSuperAdministrator, Roles.FrontProductAdministrator, Roles.FrontProductUser)]
    public class ProductController : Controller
    {
        private IProductService productService;
        private IService<Category> categoryService;
        private IUserService userService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public ProductController(IProductService productService, IService<Category> categoryService, IUserService userService,
        IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.productService = productService;
            this.categoryService = categoryService;
            this.userService = userService;
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
            var products = this.productService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<Product, bool>> predicate = null;

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
            Expression<Func<Product, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "description": order = o => o.Description; break;
                    case "price": order = o => o.Price; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<Product, ProductRow>(request, products, (p =>
            {
                var row = new ProductRow(this.localizer, this.operationContext)
                {
                    DT_RowId = p.Id.ToString(),
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price.ToString()
                };

                row.SetActions(p);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.FrontSuperAdministrator, Roles.FrontProductAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get categories
            ViewData["Categories"] = this.GetCategories();

            // Return view with empty object 
            return View("CreateOrEdit", new ProductViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.FrontSuperAdministrator, Roles.FrontProductAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Price,ReleaseDate,CategoryId,Features,EditorsIds")] ProductViewModel product)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.productService.CreateAsync(product.ToEntity());
                return RedirectToAction("Index", "Product");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get categories
            ViewData["Categories"] = this.GetCategories();

            // Get selected editors from db
            if (product.EditorsIds != null && product.EditorsIds.Count > 0)
            {
                product.Editors = new List<SelectListItem>();
                var editors = await this.userService.GetMany(product.EditorsIds);
                foreach (var item in editors)
                {
                    product.Editors.Add(new SelectListItem(item.Email, item.Id.ToString(), true));
                }
            }

            return View("CreateOrEdit", product);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.FrontSuperAdministrator, Roles.FrontProductAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get product from database 
            var product = this.productService.GetFull(new Guid(id));

            // Get categories
            ViewData["Categories"] = this.GetCategories(product.CategoryId);

            // Return view with product info
            return View("CreateOrEdit", new ProductViewModel(product));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.FrontSuperAdministrator, Roles.FrontProductAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,Description,Price,ReleaseDate,CategoryId,Features,EditorsIds")] ProductViewModel product)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.productService.UpdateAsync(product.ToEntity());
                return RedirectToAction("Index", "Product");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get categories
            ViewData["Categories"] = this.GetCategories(new Guid(product.CategoryId));

            // Get selected editors from db
            if (product.EditorsIds != null && product.EditorsIds.Count > 0)
            {
                product.Editors = new List<SelectListItem>();
                var editors = await this.userService.GetMany(product.EditorsIds);
                foreach (var item in editors)
                {
                    product.Editors.Add(new SelectListItem(item.Email, item.Id.ToString(), true));
                }
            }

            return View("CreateOrEdit", product);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            try
            {
                ViewData["Title"] = localizer[Lang.DetailsTitle];

                // Get product from database 
                var product = this.productService.GetFull(new Guid(id));
                // Return view with product info
                return View(new ProductViewModel(product));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeProductAdministrator, Roles.FrontSuperAdministrator, Roles.FrontProductAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.productService.DeleteAsync(new Guid(id));
                this.HttpContext.Response.StatusCode = 200;
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

        private List<SelectListItem> GetCategories(Guid? selectedCategory = null)
        {
            if (selectedCategory == null)
            {
                return this.categoryService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            }
            else
            {
                return this.categoryService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == selectedCategory)).ToList();
            }
        }

        // Get New Row for Feature
        [HttpGet]
        public IActionResult GetFeatureRow(int rowsLength)
        {
            var product = new ProductViewModel();
            for (int i = 0; i < rowsLength; i++)
            {
                product.Features.Add(new FeatureViewModel());
            }
            return ViewComponent("FeatureRow", new { index = rowsLength - 1, model = product });
        }

        [HttpGet]
        public async Task<JsonResult> SearchEditors(string term)
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