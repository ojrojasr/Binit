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
using System.Threading.Tasks;
using WebApp.Attributes;
using WebApp.Models;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.GameController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator, Roles.BackofficePlayUser)]
    public class GameController : Controller
    {
        private IThemeService themeService;
        private IQuestionService questionService;
        private IUserService userService;
        private IGameService gameService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;
        

        public GameController(IThemeService themeService, IQuestionService questionService, IUserService userService, IGameService gameService,
        IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.themeService = themeService;
            this.questionService = questionService;
            this.userService = userService;
            this.gameService = gameService;
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = localizer[Lang.IndexTitle];
            // This is required in order to localize datatables.
            List<ThemeViewModel> themes = GetThemes();
            return View(themes);
        }


        public List<ThemeViewModel> GetThemes()
        {
            // Get completed themes from database 
            List<Theme> themes = this.themeService.GetAllFull().ToList();
            List<ThemeViewModel> themesToSend = new List<ThemeViewModel>();

            foreach (Theme theme in themes)
            {
                themesToSend.Add(new ThemeViewModel
                {
                    Color = theme.Color,
                    Name = theme.Name,
                    Id = theme.Id.ToString(),
                    Completed = theme.QuestionQuantity > theme.Questions.Count ? false : true
                });
            }
            return themesToSend;
        }

        public async Task<JsonResult> CheckAnswer(Guid answerId)
        {
            Guid userId = this.operationContext.GetUserId();
            Question question = this.questionService.GetQuestion(answerId);
            Guid correctAnswer;
            Game game = this.gameService.GetAllFull().FirstOrDefault(g => g.ThemeId == question.ThemeId && g.Ended == false && g.UserId == userId);

            GameAnswer answerSelected = new GameAnswer
            {
                QuestionId = question.Id,
                AnswerId = answerId,
                GameId = game.Id
            };
            game.Answers.Add(answerSelected);
            await this.gameService.UpdateAsync(game);

            foreach (Answer answer in question.Answers)
            {
                if (answer.IsCorrect == true)
                {
                    correctAnswer = answer.Id;
                    if (answer.Id == answerId)
                    {
                        game.CorrectQuantity++;
                        await this.gameService.UpdateAsync(game);
                    }
                    return Json(correctAnswer);
                }
            }
            return Json("Error");
        }

        public async Task<IActionResult> Play(Guid id)
        {

            ViewData["Title"] = localizer[Lang.IndexTitle];
            // Get completed themes from database 
            if(id == Guid.Empty)
                return RedirectToAction("Index", "Game");
            Theme theme = this.themeService.GetFull(id);
            //if the theme doesnt exist and is not complete then redirect the user to the index
            if (theme.Id == null || theme.QuestionQuantity > theme.Questions.Count)
                return RedirectToAction("Index", "Game");

            //Get the user's Id
            Guid userId = this.operationContext.GetUserId();
            QuestionViewModel question;

            
            Game game = this.gameService.GetAllFull().FirstOrDefault(g => g.ThemeId == theme.Id && g.Ended == false && g.UserId == userId);

            //get random answers to play
            List<Guid> questionsToAnswer = this.questionService.GetAll().Where(q => q.ThemeId == id).Select(g => g.Id).OrderBy(q => Guid.NewGuid()).ToList();
            List<Guid> questionsAnswered = new List<Guid>();
            Guid questionSelected;
            

            if (game != null)
            {

                if (game.QuestionQuantity > game.Answers.Count)
                {
                    //Get the answers id to compare the lists
                    questionsAnswered = game.Answers.Select(q => q.QuestionId).ToList();

                    //Delete the answers that are already answered
                    var NewQTA = questionsToAnswer.Except(questionsAnswered);
                    
                    questionSelected = NewQTA.First();

                    //get the question by the id
                    Question realQuestion = questionService.GetFull(questionSelected);
                    question = new QuestionViewModel(realQuestion, theme.Color);

                    return View(question);

                }
                else
                    return RedirectToAction("Result", "Game", new { themeId = theme.Id });
                

            }
            else
            {
                game = new Game
                {
                    UserId = userId,
                    Ended = false,
                    Answers = new List<GameAnswer>(),
                    ThemeId = id,
                    QuestionQuantity = theme.QuestionQuantity,
                    CorrectQuantity = 0,
                    Id = new Guid()
                };

                questionsAnswered = game.Answers.Select(q => q.QuestionId).ToList();

                questionSelected = questionsToAnswer.First();

                Question realQuestion = questionService.GetFull(questionSelected);
                question = new QuestionViewModel(realQuestion, theme.Color);

                await this.gameService.CreateAsync(game);
                return View(question);  
            }

        }


        public IActionResult Result(Guid themeId)
        {

            if (themeId == null)
                return RedirectToAction("Index", "Game");
            ViewData["Title"] = localizer[Lang.ResultTitle];
            Guid userId = this.operationContext.GetUserId();

            //looking the user's name
            string usersName = this.userService.GetAll().FirstOrDefault(u => u.Id == userId).Name;
            string usersLastName = this.userService.GetAll().FirstOrDefault(u => u.Id == userId).LastName;
            string userName = usersName + ' ' + usersLastName;
            //searching the game that just ended and finish it
            Game realGame = this.gameService.GetAllFull().FirstOrDefault(g => g.ThemeId == themeId && g.Ended == false && g.UserId == userId);
            if (realGame == null)
                return RedirectToAction("Index", "Game");
            realGame.Ended = true;
            this.gameService.Update(realGame);

            // set the gameVM
            GameViewModel game = new GameViewModel
            {
                UserName = userName,
                Theme = realGame.Theme.Name,
                Questions = new List<QuestionViewModel>(),
                QuestionQuantity = realGame.QuestionQuantity,
                QuestionCorrect = realGame.CorrectQuantity,
                AnswersSelected = new List<string>()
            };

            foreach (GameAnswer answer in realGame.Answers)
            {
                Question question = this.questionService.GetQuestion(answer.AnswerId);
                string color = this.themeService.GetFull(themeId).Color;
                game.AnswersSelected.Add(answer.AnswerId.ToString());
                game.Questions.Add(new QuestionViewModel(question, color));
            }

            return View(game);
        }

            
    }

    
}