using Domain.Entities.Model;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.ApplicationUserDTOs.CreateUserReq;

namespace WebAPI.DTOs.ApplicationUserDTOs
{
    public class CreateUserReq
    {
        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        [StringLength(50, ErrorMessage = Lang.NameStringLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.LastNameRequired)]
        [StringLength(50, ErrorMessage = Lang.LastNameStringLength)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Lang.ConfirmEmailCallbackRequired)]
        public string ConfirmEmailCallback { get; set; }

        [Required(ErrorMessage = Lang.RolesRequired)]
        public string[] Roles { get; set; }

        public ApplicationUser ToApplicationUser()
        {
            return new ApplicationUser
            {
                UserName = this.Email,
                Email = this.Email,
                Name = this.Name,
                LastName = this.LastName
            };
        }
    }
}