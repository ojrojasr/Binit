using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.HolidayTypeViewModel;

namespace WebApp.Models
{
    public class HolidayTypeViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [Display(Name = Lang.DescriptionLabel)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        public HolidayTypeViewModel()
        {

        }

        public HolidayTypeViewModel(HolidayType holidayType)
        {
            this.Id = holidayType.Id.ToString();
            this.Name = holidayType.Name;
            this.Description = holidayType.Description;
        }


        public HolidayType ToEntity()
        {
            return new HolidayType()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description
            };
        }
    }
}