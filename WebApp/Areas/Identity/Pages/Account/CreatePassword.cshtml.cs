using Binit.Framework.ExceptionHandling.Types;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.CreatePassword;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class CreatePasswordModel : PageModel
    {
        private readonly IAccountService accountService;

        public CreatePasswordModel(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        public string UserId { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.PasswordRequired)]
            [StringLength(100, ErrorMessage = Lang.PasswordStringLength, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = Lang.PasswordLabel)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = Lang.ConfirmPasswordLabel)]
            [Compare("Password", ErrorMessage = Lang.ConfirmPasswordCompare)]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGet(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                try
                {
                    await accountService.CreatePassword(UserId, Code, Input.Password);
                    return RedirectToPage("./CreatePasswordConfirmation");
                }
                catch (IdentityException idex)
                {
                    foreach (var error in idex.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
