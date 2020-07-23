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
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.QuestionController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
    public class QuestionController : Controller
    {
        private IQuestionService questionService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;
        private readonly IThemeService themeService;

        public QuestionController(IQuestionService questionService, IThemeService themeService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.questionService = questionService;
            this.localizer = localizer;
            this.operationContext = operationContext;
            this.themeService = themeService;
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

            var questions = this.questionService.GetAllFull();
            var searchTerm = request.search.value;
            Expression<Func<Question, bool>> predicate = null;

            // If search term is not empty...
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = p =>
                (
                    p.Name.Contains(searchTerm)
                );
            }

            // Order
            Expression<Func<Question, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "theme": order = o => o.Theme.Name; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<Question, QuestionRow>(request, questions, (c =>
            {
                var row = new QuestionRow(this.localizer, this.operationContext)
                {
                    DT_RowId = c.Id.ToString(),
                    Name = c.Name,
                   
                };

                row.SetActions(c);
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
            ViewData["Themes"] = this.GetThemes();
            // Return view with empty object 
            return View("CreateOrEdit", new QuestionViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create( QuestionViewModel question)
        {
            // Check if model is valid
            ValidateQuestion(question);
            if (ModelState.IsValid)
            {
                await this.questionService.CreateAsync(question.ToEntity());
                return RedirectToAction("Index", "Question");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";
            ViewData["Themes"] = this.GetThemes();

            return View("CreateOrEdit", question);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (!this.questionService.GetAll().Any(t => t.Id.ToString() == id))
                return RedirectToAction("Index", "Question");
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get category from database 
            var question = this.questionService.GetFull(new Guid(id));
            ViewData["Themes"] = this.GetThemes(question.ThemeId);

            // Return view with category info
            return View("CreateOrEdit", new QuestionViewModel(question));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit( QuestionViewModel question)
        {
            // Check if model is valid
            ValidateQuestion(question);
            if (ModelState.IsValid)
            {
                await this.questionService.UpdateAsync(question.ToEntity());
                return RedirectToAction("Index", "Question");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            ViewData["Themes"] = this.GetThemes(new Guid(question.ThemeId));

            return View("CreateOrEdit", question);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            if (!this.questionService.GetAll().Any(t => t.Id.ToString() == id))
                return RedirectToAction("Index", "Question");

            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get category from database 
            var question = this.questionService.GetFull(new Guid(id));

            // Return view with category info
            return View(new QuestionViewModel(question));
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.questionService.DeleteAsync(new Guid(id));
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

        private List<SelectListItem> GetThemes(Guid? selectedTheme = null)
        {
            if (selectedTheme == null)
            {
                return this.themeService.GetAll().OrderBy(t => t.Name)
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            }
            else
            {
                return this.themeService.GetAll().OrderBy(t => t.Name)
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == selectedTheme)).ToList();
            }
        }


        [HttpGet]
        public IActionResult GetAnswerRow(int rowsLength)
        {
            var question = new QuestionViewModel();
            for (int i = 0; i < rowsLength; i++)
            {
                question.Answers.Add(new AnswerViewModel());
            }
            return ViewComponent("AnswerRow", new { index = rowsLength - 1, model = question });
        }


        public void ValidateQuestion(QuestionViewModel model)
        {
            if (model.Answers.Count != 4)
                ModelState.AddModelError("Answers", localizer[Lang.FourAnswers]);
            else
            {
                int correctas = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (model.Answers[i].IsCorrect)
                        correctas++;
                }
                if (correctas != 1)
                    ModelState.AddModelError("Answers", localizer[Lang.OneCorrect]);
            }

        }
        #endregion
    }
}