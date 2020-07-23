using Binit.Framework.Constants;
using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.TenantViewModel;

namespace WebApp.Models
{
    public class TenantViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.CodeRequired)]
        [Display(Name = Lang.CodeLabel)]
        [RegularExpression(RegularExpressions.NoSpecialCharacters, ErrorMessage = Lang.CodeRegexError)]
        public string Code { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [Display(Name = Lang.DescriptionLabel)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        public TenantViewModel()
        {
        }

        public TenantViewModel(Tenant tenant)
        {
            this.Id = tenant.Id.ToString();
            this.Code = tenant.Code;
            this.Name = tenant.Name;
            this.Description = tenant.Description;
        }

        public Tenant ToEntity()
        {
            var tenant = new Tenant()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Code = this.Code,
                Name = this.Name,
                Description = this.Description
            };

            return tenant;
        }
    }
}