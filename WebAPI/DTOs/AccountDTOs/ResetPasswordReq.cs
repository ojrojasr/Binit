using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.ResetPasswordReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class ResetPasswordReq
    {
        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.PasswordRequired)]
        [StringLength(100, ErrorMessage = Lang.PasswordStringLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = Lang.ConfirmPasswordCompare)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = Lang.CodeRequired)]
        public string Code { get; set; }
    }
}