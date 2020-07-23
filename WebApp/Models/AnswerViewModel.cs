using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.FeatureViewModel;

namespace WebApp.Models
{
    public class AnswerViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }
        public bool IsCorrect { get; set; }

        public AnswerViewModel()
        {
            this.Id = new Guid().ToString();
        }

        public AnswerViewModel(Answer answer)
        {
            this.Id = answer.Id.ToString();
            this.Name = answer.Name;
            this.IsCorrect = answer.IsCorrect;
        }

        public Answer ToEntity()
        {
            return new Answer()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                Name = this.Name,
                IsCorrect = this.IsCorrect
            };
        }
    }
}