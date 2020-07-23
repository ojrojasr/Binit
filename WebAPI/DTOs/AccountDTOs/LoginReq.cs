using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.LoginReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class LoginReq
    {
        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.PasswordRequired)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}