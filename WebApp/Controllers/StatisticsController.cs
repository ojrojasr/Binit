using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.DTOs;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.StatisticsController;

namespace WebApp.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private IStatisticsBusinessLogic statisticsBusinessLogic;
        private IGameService gameService;
        private IThemeService themeService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;



        public StatisticsController(IStatisticsBusinessLogic statisticsBusinessLogic, IThemeService themeService, IGameService gameService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.gameService = gameService;
            this.themeService = themeService;
            this.statisticsBusinessLogic = statisticsBusinessLogic;
            this.localizer = localizer;
            this.operationContext = operationContext;
        }



        public IActionResult Index()
        {
            var userId = this.operationContext.GetUserId();

            var model = this.statisticsBusinessLogic.GetCardDTO(userId);

            return View(model);
        }
        public ActionResult GraphicPie()
        {
            var userId = this.operationContext.GetUserId();
            
            IQueryable<Game> games = gameService.GetAllFull();

            if (!this.operationContext.UserIsInRole(Roles.BackofficeSuperAdministrator))
                games = games.Where(g => g.UserId == userId);

            var model = new GraphicPieDTO();
            var totalGames = games.Where(g => g.UserId == userId).Count();
            model.colors = new List<string>();
            model.title = localizer[Lang.PieChart];
            model.series = new List<PieDTO>();
            var themes = this.themeService.GetAllFull().ToList();
            
            foreach (var theme in themes)
            {
                double percentage = 0;
                if (theme.QuestionQuantity <= theme.Questions.Count()) {
                    var totalGamesByTheme = games.Where(g => g.ThemeId == theme.Id).Count();
                    if(totalGames != 0)
                        percentage = (totalGamesByTheme * 100) / totalGames;

                    model.colors.Add(theme.Color);
                    model.series.Add(new PieDTO
                    {
                        name = theme.Name,
                        y = percentage
                    });
                }
                

            }

            return Json(model);
        }
        public ActionResult GraphicBar()
        {
            var userId = this.operationContext.GetUserId();
            var model = new GraphicBarDTO();
            IQueryable<Game> games = gameService.GetAllFull();

            if (!this.operationContext.UserIsInRole(Roles.BackofficeSuperAdministrator))
                games = games.Where(g => g.UserId == userId);
            model.categories = new List<string>();
            model.colors = new List<string>();
            model.title = localizer[Lang.BarChart];
            model.subtitle = "";
            model.series = new List<double>();
            var themes = this.themeService.GetAllFull().ToList();

            foreach (var theme in themes)
            {
                if (theme.QuestionQuantity <= theme.Questions.Count())
                {
                    var totalPerfectGamesByTheme = games.Where(g => g.ThemeId == theme.Id && g.CorrectQuantity == g.QuestionQuantity).Count();
                    model.categories.Add(theme.Name);
                    model.series.Add(totalPerfectGamesByTheme);
                    model.colors.Add(theme.Color);
                }
            }

            return Json(model);
        }
        public ActionResult GraphicInverted()
        {
            var userId = this.operationContext.GetUserId();
            var model = new GraphicBarDTO();
            IQueryable<Game> games = gameService.GetAllFull();

            if (!this.operationContext.UserIsInRole(Roles.BackofficeSuperAdministrator))
                games = games.Where(g => g.UserId == userId);
            model.categories = new List<string>();
            model.colors = new List<string>();
            model.title = localizer[Lang.InvertChart];
            model.subtitle = "";
            model.series = new List<double>();
            var themes = this.themeService.GetAllFull().ToList();
            
            foreach (var theme in themes)
            {
                double percentage = 0;
                if (theme.QuestionQuantity <= theme.Questions.Count())
                {
                    var totalCorrectAnswersByTheme = games.Where(g => g.ThemeId == theme.Id).Sum(a => a.CorrectQuantity);
                    var totalAnswersByTheme = games.Where(g => g.ThemeId == theme.Id).SelectMany(g => g.Answers).Count();
                    if (totalAnswersByTheme != 0)
                        percentage = (totalCorrectAnswersByTheme * 100) / totalAnswersByTheme;   
                    model.categories.Add(theme.Name);
                    model.series.Add(percentage);
                    model.colors.Add(theme.Color);
                }
            }

            return Json(model);
        }
    }
}
