using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.ForgotPasswordReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class ForgotPasswordReq
    {
        [Required(ErrorMessage = Lang.EmailRequired)]
        [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
        public string Email { get; set; }

        [Required(ErrorMessage = Lang.ForgotPasswordEmailCallbackRequired)]
        public string ForgotPasswordEmailCallback { get; set; }
    }
}