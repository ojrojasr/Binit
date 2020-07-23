using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.FeatureViewModel;

namespace WebApp.Models
{
    public class FeatureViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        [Display(Name = Lang.DescriptionLabel)]
        public string Description { get; set; }

        public FeatureViewModel()
        {
            this.Id = new Guid().ToString();
        }

        public FeatureViewModel(Feature feature)
        {
            this.Id = feature.Id.ToString();
            this.Name = feature.Name;
            this.Description = feature.Description;
        }

        public Feature ToEntity()
        {
            return new Feature()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}