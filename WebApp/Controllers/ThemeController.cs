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
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.ThemeController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
    public class ThemeController : Controller
    {
        private IThemeService themeService;
        private IService<Question> questionService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public ThemeController(IThemeService themeService, IService<Question> questionService,
        IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.themeService = themeService;
            this.questionService = questionService;
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
            var theme = this.themeService.GetAll();
            string searchTerm = request.search.value;
            Expression<Func<Theme, bool>> predicate = null;

            // If search term is not empty...
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = p =>
                (
                    p.Name.Contains(searchTerm) ||
                    p.Color.Contains(searchTerm)
                );
            }

            // Order
            Expression<Func<Theme, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                string orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "color": order = o => o.Color; break;
                    case "questionQuantity": order = o => o.QuestionQuantity; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<Theme, ThemeRow>(request, theme, (p =>
            {
                ThemeRow row = new ThemeRow(this.localizer, this.operationContext)
                {
                    DT_RowId = p.Id.ToString(),
                    Name = p.Name,
                    Color = p.Color,
                    QuestionQuantity = p.QuestionQuantity.ToString()
                };

                row.SetActions(p);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get categories
            //ViewData["Questions"] = this.GetQuestions();

            // Return view with empty object 
            return View("CreateOrEdit", new ThemeViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create(ThemeViewModel theme)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.themeService.CreateAsync(theme.ToEntity());
                return RedirectToAction("Index", "Theme");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get categories
            //ViewData["Questions"] = this.GetQuestions();

            return View("CreateOrEdit", theme);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!this.themeService.GetAll().Any(t => t.Id.ToString() == id))
                return RedirectToAction("Index", "Theme");

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get theme from database 
            Theme theme = this.themeService.GetAll().FirstOrDefault(t => t.Id.ToString() == id);

            // Return view with themes info
            return View("CreateOrEdit", new ThemeViewModel(theme));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit(ThemeViewModel theme)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.themeService.UpdateAsync(theme.ToEntity());
                return RedirectToAction("Index", "Theme");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            return View("CreateOrEdit", theme);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            if (!this.themeService.GetAll().Any(t => t.Id.ToString() == id))
                return RedirectToAction("Index", "Theme");
            try
            {
                ViewData["Title"] = localizer[Lang.DetailsTitle];

                // Get theme from database 
                Theme theme = this.themeService.GetFull(new Guid(id));
                // Return view with theme info
                return View(new ThemeViewModel(theme));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.themeService.DeleteAsync(new Guid(id));
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

        private List<SelectListItem> GetQuestion(Guid? selectedQuestion = null)
        {
            if (selectedQuestion == null)
            {
                return this.questionService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            }
            else
            {
                return this.questionService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == selectedQuestion)).ToList();
            }
        }

    }
}