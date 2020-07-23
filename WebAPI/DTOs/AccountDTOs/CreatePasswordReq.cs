using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.CreatePasswordReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class CreatePasswordReq
    {

        [Required(ErrorMessage = Lang.UserIdRequired)]
        public string UserId { get; set; }

        [Required(ErrorMessage = Lang.CodeRequired)]
        public string Code { get; set; }

        [Required(ErrorMessage = Lang.NewPasswordRequired)]
        [StringLength(100, ErrorMessage = Lang.NewPasswordStringLength, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = Lang.ConfirmPasswordCompare)]
        public string ConfirmPassword { get; set; }
    }
}