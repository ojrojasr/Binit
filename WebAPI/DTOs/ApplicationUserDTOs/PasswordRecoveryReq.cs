using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.ApplicationUserDTOs.PasswordRecoveryReq;

namespace WebAPI.DTOs.ApplicationUserDTOs
{
    public class PasswordRecoveryReq
    {
        [Required(ErrorMessage = Lang.IdRequired)]
        public string Id { get; set; }

        [Required(ErrorMessage = Lang.RecoveryEmailCallbackRequired)]
        public string RecoveryEmailCallback { get; set; }

    }
}