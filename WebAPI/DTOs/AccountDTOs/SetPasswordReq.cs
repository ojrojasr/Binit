using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.SetPasswordReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class SetPasswordReq
    {
        [Required(ErrorMessage = Lang.NewPasswordRequired)]
        [StringLength(100, ErrorMessage = Lang.NewPasswordStringLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = Lang.ConfirmPasswordCompare)]
        public string ConfirmPassword { get; set; }
    }
}