using Binit.Framework;
using Binit.Framework.Interfaces.DAL;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.Login;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public LoginModel(IAccountService accountService, IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.accountService = accountService;
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.EmailRequired)]
            [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
            [Display(Name = Lang.EmailLabel)]
            public string Email { get; set; }

            [Required(ErrorMessage = Lang.PasswordRequired)]
            [Display(Name = Lang.PasswordLabel)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = Lang.RememberMe)]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = string.IsNullOrEmpty(returnUrl) ? Url.Content("~/") : returnUrl;

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = await accountService.GetExternalAuthenticationSchemes();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var user = await accountService.GetUser(Input.Email);
                if (user != null)
                {
                    var userRoles = await accountService.GetRoles(user);

                    if (operationContext.RolesBelongToRealm(userRoles))
                    {
                        var result = await accountService.Login(Input.Email, Input.Password, Input.RememberMe);

                        if (result.Succeeded)
                        {
                            returnUrl = string.IsNullOrEmpty(returnUrl) ? Url.Content("~/") : returnUrl;
                            return LocalRedirect(returnUrl);
                        }

                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }

                        if (result.IsLockedOut)
                        {
                            return RedirectToPage("./Lockout");
                        }

                        // If the user didn't confirm the account...
                        if (result.IsNotAllowed)
                        {
                            ModelState.AddModelError(string.Empty, localizer[Lang.NotAllowedAttempt]);
                            ExternalLogins = await accountService.GetExternalAuthenticationSchemes();
                            return Page();
                        }
                    }
                }

                ExternalLogins = await accountService.GetExternalAuthenticationSchemes();
                ModelState.AddModelError(string.Empty, localizer[Lang.InvalidLoginAttempt]);
                return Page();
            }

            ExternalLogins = await accountService.GetExternalAuthenticationSchemes();
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
