using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.ChangePassword;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public ChangePasswordModel(IAccountService accountService, IStringLocalizer<SharedResources> localizer)
        {
            this.accountService = accountService;
            this.localizer = localizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.OldPasswordRequired)]
            [DataType(DataType.Password)]
            [Display(Name = Lang.OldPasswordLabel)]
            public string OldPassword { get; set; }

            [Required(ErrorMessage = Lang.NewPasswordRequired)]
            [StringLength(100, ErrorMessage = Lang.NewPasswordStringLength, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = Lang.NewPasswordLabel)]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = Lang.ConfirmNewPasswordLabel)]
            [Compare("NewPassword", ErrorMessage = Lang.ConfirmPasswordCompare)]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            bool hasPassword = false;
            try
            {
                hasPassword = await accountService.UserHasPassword();
            }
            catch (NotFoundException)
            {
                return NotFound(this.localizer[Lang.CannotLoadUser]);
            }

            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await this.accountService.ChangePassword(Input.OldPassword, Input.NewPassword);

                StatusMessage = this.localizer[Lang.PasswordChanged];
                return RedirectToPage();
            }
            catch (NotFoundException)
            {
                return NotFound(this.localizer[Lang.CannotLoadUser]);
            }
            catch (IdentityException idex)
            {
                // If there're specific errors to report, return them.
                foreach (var error in idex.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
