using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.ApplicationUserDTOs.ApplicationUserDTO;

namespace WebAPI.DTOs.ApplicationUserDTOs
{
    public class ApplicationUserDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        [StringLength(50, ErrorMessage = Lang.NameStringLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.LastNameRequired)]
        [StringLength(50, ErrorMessage = Lang.LastNameStringLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Lang.RolesRequired)]
        public string[] Roles { get; set; }

        public ApplicationUserDTO(ApplicationUser user)
        {
            this.Id = user.Id.ToString();
            this.Email = user.Email;
            this.Name = user.Name;
            this.LastName = user.LastName;
        }

        public ApplicationUserDTO(ApplicationUser user, string[] roles)
            : this(user)
        {
            this.Roles = roles;
        }

        public ApplicationUser ToEntity()
        {
            var entity = new ApplicationUser
            {
                UserName = this.Email,
                Email = this.Email,
                Name = this.Name,
                LastName = this.LastName
            };

            if (!string.IsNullOrEmpty(this.Id))
                entity.Id = new Guid(this.Id);

            return entity;
        }
    }
}