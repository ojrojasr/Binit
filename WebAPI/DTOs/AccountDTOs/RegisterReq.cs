using Domain.Entities.Model;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.RegisterReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class RegisterReq
    {
        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.PasswordRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        [StringLength(50, ErrorMessage = Lang.NameStringLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.LastNameRequired)]
        [StringLength(50, ErrorMessage = Lang.LastNameRequired)]
        public string LastName { get; set; }

        [Required(ErrorMessage = Lang.ConfirmEmailCallbackRequired)]
        public string ConfirmEmailCallback { get; set; }

        public ApplicationUser ToEntity()
        {
            var entity = new ApplicationUser
            {
                UserName = this.Email,
                Email = this.Email,
                Name = this.Name,
                LastName = this.LastName
            };

            return entity;
        }
    }
}