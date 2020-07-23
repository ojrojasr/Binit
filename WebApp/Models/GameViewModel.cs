using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.GameViewModel;

namespace WebApp.Models
{
    public class GameViewModel : EntityViewModel
    {
        public string UserName { get; set; }
        public List<QuestionViewModel> Questions {get; set;}
        public List<string> AnswersSelected { get; set; }
        public string Theme { get; set; }
        public int QuestionQuantity { get; set; }
        public int QuestionCorrect { get; set; }

        public GameViewModel()
        {
            this.Id = new Guid().ToString();
            this.Questions = new List<QuestionViewModel>();
            this.AnswersSelected = new List<string>();
        }

        public GameViewModel(Game game)
        {
            this.Id = game.Id.ToString();
            this.Questions = new List<QuestionViewModel>();
            this.AnswersSelected = new List<string>();

            if (game.Id != null)
            {
                this.Theme = game.Theme.Name;
            }


        }

    }
}