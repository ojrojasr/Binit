using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.ExternalLogin;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly IUserService userService;
        private readonly IAccountService accounService;
        private readonly IStringLocalizer<SharedResources> localizer;


        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IUserService userService,
            IAccountService accounService,
            IStringLocalizer<SharedResources> localizer)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            this.userService = userService;
            this.accounService = accounService;
            this.localizer = localizer;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string LoginProvider { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.EmailRequired)]
            [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
            [Display(Name = Lang.EmailLabel)]
            public string Email { get; set; }

            [Required(ErrorMessage = Lang.NameRequired)]
            [Display(Name = Lang.NameLabel)]
            public string Name { get; set; }

            [Required(ErrorMessage = Lang.LastNameRequired)]
            [Display(Name = Lang.LastNameLabel)]
            public string LastName { get; set; }

            public bool AllowSetEmail { get; set; }

            public bool AccountExists { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = string.Format(localizer[Lang.ExternalProviderError], remoteError);
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = localizer[Lang.ExternalLoginError];
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                LoginProvider = info.LoginProvider;

                Input = new InputModel();

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                    Input.AllowSetEmail = false;

                    var dbUser = await this.accounService.GetUser(Input.Email);
                    if (dbUser != null)
                    {
                        Input.AccountExists = true;

                        Input.Name = dbUser.Name;
                        Input.LastName = dbUser.LastName;
                    }
                }
                else
                {
                    Input.AllowSetEmail = true;
                }

                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = localizer[Lang.ExternalLoginErrorOnConfirmation];
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new FrontUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    LastName = Input.LastName,
                    EmailConfirmed = true,
                    TenantId = Tenants.MasterId
                };

                try
                {
                    var dbUser = await this.accounService.GetUser(user.Email);
                    if (dbUser == null)
                    {
                        // Create user if not exists.
                        await this.userService.CreateAsync(user, new List<string> {
                            Roles.FrontEventUser,
                            Roles.FrontHolidayUser,
                            Roles.FrontProductUser,
                            Roles.FrontCategoryUser,
                            Roles.FrontHolidayTypeUser
                        });
                    }
                    else
                    {
                        user = (FrontUser)dbUser;
                        user.EmailConfirmed = true;
                    }

                    var result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }
                catch (IdentityException ex)
                {
                    foreach (var error in ex.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            LoginProvider = info.LoginProvider;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
