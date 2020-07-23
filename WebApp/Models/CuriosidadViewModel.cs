using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.CuriosidadViewModel;

namespace WebApp.Models
{
    public class CuriosidadViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        [Display(Name = Lang.DescriptionLabel)]
        public string Description { get; set; }

        public CuriosidadViewModel()
        {
            this.Id = new Guid().ToString();
        }

        public CuriosidadViewModel(Curiosidad curiosidad)
        {
            this.Id = curiosidad.Id.ToString();
            this.Name = curiosidad.Name;
            this.Description = curiosidad.Description;
        }

        public Curiosidad ToEntity()
        {
            return new Curiosidad()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}