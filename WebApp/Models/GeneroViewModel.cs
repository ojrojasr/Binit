using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.GeneroViewModel;

namespace WebApp.Models
{
    public class GeneroViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [Display(Name = Lang.DescriptionLabel)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        public GeneroViewModel()
        {

        }

        public GeneroViewModel(Genero genero)
        {
            this.Id = genero.Id.ToString();
            this.Name = genero.Name;
            this.Description = genero.Description;
        }

        public Genero ToEntity()
        {
            return new Genero()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}