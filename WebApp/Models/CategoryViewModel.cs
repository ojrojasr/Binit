using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.CategoryViewModel;

namespace WebApp.Models
{
    public class CategoryViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [Display(Name = Lang.DescriptionLabel)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category)
        {
            this.Id = category.Id.ToString();
            this.Name = category.Name;
            this.Description = category.Description;
        }

        public Category ToEntity()
        {
            return new Category()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}