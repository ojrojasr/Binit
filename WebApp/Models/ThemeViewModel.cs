using Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.ThemeViewModel;

namespace WebApp.Models
{
    public class ThemeViewModel : EntityViewModel
    {

        [Required(ErrorMessage = Lang.NameRequired)]
        [StringLength(200, ErrorMessage = Lang.NameStringLength, MinimumLength = 5)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.ColorRequired)]
        [StringLength(7, ErrorMessage = Lang.ColorStringLength, MinimumLength = 7)]
        [Display(Name = Lang.ColorLabel)]
        public string Color { get; set; }
        public bool Completed { get; set; }
        [Range(1,20)]
        [Display(Name = Lang.QuestionQuantityLabel)]
        public int QuestionQuantity { get; set; }
        public List<Question> Questions { get; set; }

        public ThemeViewModel()
        {
            this.Id = new Guid().ToString();
            this.Questions = new List<Question>();
        }

        public ThemeViewModel(Theme theme)
        {
            this.Id = theme.Id.ToString();
            this.Name = theme.Name;
            this.Color = theme.Color;
            this.QuestionQuantity = theme.QuestionQuantity;
            this.Questions = new List<Question>();
        }

        public Theme ToEntity()
        {
            var theme = new Theme()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                QuestionQuantity = this.QuestionQuantity,
                Color = this.Color
            };

            return theme;
        }
    }
}