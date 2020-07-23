using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.AccountDTOs.ConfirmEmailReq;

namespace WebAPI.DTOs.AccountDTOs
{
    public class ConfirmEmailReq
    {
        [Required(ErrorMessage = Lang.UserIdRequired)]
        public string UserId { get; set; }

        [Required(ErrorMessage = Lang.CodeRequired)]
        public string Code { get; set; }
    }
}