using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.QuestionViewModel;

namespace WebApp.Models
{
    public class QuestionViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }
        [Display(Name = Lang.AnswersLabel)]
        public List<AnswerViewModel> Answers {get; set;}
        [Display(Name = Lang.ThemeLabel)]
        [Required(ErrorMessage = Lang.NameRequired)]
        public string ThemeId { get; set; }
        [Display(Name = Lang.ThemeLabel)]
        public string Theme { get; set; }
        public string Color { get; set; }

        public QuestionViewModel()
        {
            this.Id = new Guid().ToString();
            this.Answers = new List<AnswerViewModel>();
        }

        public QuestionViewModel(Question question)
        {
            this.Id = question.Id.ToString();
            this.Name = question.Name;
            this.Answers = new List<AnswerViewModel>();

            if (question.Id != null)
            {
                this.ThemeId = question.ThemeId.ToString();
                this.Theme = question.Theme.Name;
            }

            if (question.Theme != null && question.Answers.Count > 0)
            {
                foreach (var answer in question.Answers)
                {
                    this.Answers.Add(new AnswerViewModel(answer));
                }
            }

        }
        public QuestionViewModel(Question question, string color)
        {
            this.Id = question.Id.ToString();
            this.Name = question.Name;
            this.Answers = new List<AnswerViewModel>();
            this.Color = color;

            if (question.Id != null)
            {
                this.ThemeId = question.ThemeId.ToString();
                this.Theme = question.Theme.Name;
            }

            if (question.Theme != null && question.Answers.Count > 0)
            {
                foreach (var answer in question.Answers)
                {
                    this.Answers.Add(new AnswerViewModel(answer));
                }
            }

        }


        public Question ToEntity()
        {
            var question = new Question()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Answers = new List<Answer>(),
                ThemeId = new Guid(this.ThemeId)
            };


            if (this.Answers != null && this.Answers.Count > 0)
            {
                foreach (var answer in this.Answers)
                {
                    question.Answers.Add(new Answer()
                    {
                        Id = new Guid(),
                        QuestionId = question.Id,
                        Name = answer.Name,
                        IsCorrect = answer.IsCorrect
                    });
                }
            }

            return question;
        }
    }
}