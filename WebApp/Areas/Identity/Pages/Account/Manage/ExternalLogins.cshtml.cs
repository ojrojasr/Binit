using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Interfaces.Configuration;
using Binit.Framework.Interfaces.ExceptionHandling;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExceptionsLang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.ExceptionHandling;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.ExternalLogins;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class ExternalLoginsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IAccountService accountService;
        private readonly IRealmConfiguration realmConfiguration;
        private readonly IExceptionManager exceptionManager;


        public ExternalLoginsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IAccountService accountService,
            IRealmConfiguration realmConfiguration,
            IStringLocalizer<SharedResources> localizer,
            IExceptionManager exceptionManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.accountService = accountService;
            this.localizer = localizer;
            this.realmConfiguration = realmConfiguration;
            this.exceptionManager = exceptionManager;
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<AuthenticationScheme> OtherLogins { get; set; }

        public bool ShowRemoveButton { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // If realm doesn't allow self sign up, throw NotFound exception.
            if (!realmConfiguration.AllowSelfSignUp)
                throw this.exceptionManager.Handle(new NotFoundException(localizer[ExceptionsLang.ResourceNotFoundGenericEx]));

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            CurrentLogins = await _userManager.GetLoginsAsync(user);
            OtherLogins = (await accountService.GetExternalAuthenticationSchemes())
                .Where(auth => CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            ShowRemoveButton = user.PasswordHash != null || CurrentLogins.Count > 1;
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveLoginAsync(string loginProvider, string providerKey)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
            if (!result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException(string.Format(this.localizer[Lang.ErrorRemovingExternalService], userId));
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = this.localizer[Lang.ExternalLoginRemoved];
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostLinkLoginAsync(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = Url.Page("./ExternalLogins", pageHandler: "LinkLoginCallback");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, _userManager.GetUserId(User));
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetLinkLoginCallbackAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync(await _userManager.GetUserIdAsync(user));
            if (info == null)
            {
                throw new InvalidOperationException(string.Format(this.localizer[Lang.ErrorLoadingExternalService], user.Id));
            }

            var result = await _userManager.AddLoginAsync(user, info);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Format(this.localizer[Lang.ErrorAddingExternalService], user.Id));
            }

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            StatusMessage = this.localizer[Lang.ExternalLoginAdded];
            return RedirectToPage();
        }
    }
}
