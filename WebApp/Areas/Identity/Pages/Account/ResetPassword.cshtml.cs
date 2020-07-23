using Binit.Framework.ExceptionHandling.Types;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.ResetPassword;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly IAccountService accountService;

        public ResetPasswordModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.EmailRequired)]
            [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
            [Display(Name = Lang.EmailLabel)]
            public string Email { get; set; }

            [Required(ErrorMessage = Lang.PasswordRequired)]
            [StringLength(100, ErrorMessage = Lang.PasswordStringLength, MinimumLength = 6)]
            [Display(Name = Lang.PasswordLabel)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = Lang.ConfirmPasswordLabel)]
            [Compare("Password", ErrorMessage = Lang.ConfirmPasswordCompare)]
            public string ConfirmPassword { get; set; }

            [Display(Name = Lang.CodeLabel)]
            public string Code { get; set; }
        }

        public IActionResult OnGet(string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Validates code and resets password.
                await this.accountService.ResetPassword(Input.Email, Input.Code, Input.Password);

                return RedirectToPage("./ResetPasswordConfirmation");
            }
            catch (NotFoundException)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }
            catch (IdentityException idex)
            {
                foreach (var error in idex.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
        }
    }
}
