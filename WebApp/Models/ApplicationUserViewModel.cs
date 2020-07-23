using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.ApplicationUserViewModel;

namespace WebApp.Models
{
    public class ApplicationUserViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        [Display(Name = Lang.EmailLabel)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        [StringLength(50, ErrorMessage = Lang.NameStringLength)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.LastNameRequired)]
        [StringLength(50, ErrorMessage = Lang.LastNameStringLength)]
        [Display(Name = Lang.LastNameLabel)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Lang.PhoneNumberRequired)]
        [Display(Name = Lang.PhoneNumberLabel)]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = Lang.RolesRequired)]
        [Display(Name = Lang.RolesLabel)]
        public List<string> Roles { get; set; }

        [Required(ErrorMessage = Lang.TenantRequired)]
        [Display(Name = Lang.TenantsLabel)]
        public string TenantId { get; set; }
        public string Tenant { get; set; }

        public ApplicationUserViewModel(ApplicationUser user)
        {
            this.Id = user.Id.ToString();
            this.Email = user.Email;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.PhoneNumber = user.PhoneNumber;

            if (user.Tenant != null)
            {
                this.TenantId = user.TenantId.ToString();
                this.Tenant = user.Tenant.Name;
            }
        }

        public ApplicationUserViewModel(ApplicationUser user, List<string> roles)
            : this(user)
        {
            this.Roles = roles;
            this.TenantId = user.TenantId.ToString();
            this.Tenant = user.Tenant.Name;
        }

        public ApplicationUserViewModel()
        {

        }

        public ApplicationUser ToEntity()
        {
            var entity = new ApplicationUser
            {
                UserName = this.Email,
                Email = this.Email,
                Name = this.Name,
                LastName = this.LastName,
                PhoneNumber = this.PhoneNumber,
                TenantId = new Guid(this.TenantId)
            };

            if (!string.IsNullOrEmpty(this.Id))
                entity.Id = new Guid(this.Id);

            return entity;
        }
    }

}