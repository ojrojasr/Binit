using Binit.Framework;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.TwoFactorAuthentication;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private const string AuthenicatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;
        private readonly IStringLocalizer<SharedResources> localizer;

        public TwoFactorAuthenticationModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<TwoFactorAuthenticationModel> logger,
            IStringLocalizer<SharedResources> localizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.localizer = localizer;
        }

        public bool HasAuthenticator { get; set; }

        public int RecoveryCodesLeft { get; set; }

        [BindProperty]
        public bool Is2faEnabled { get; set; }

        public bool IsMachineRemembered { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            await _signInManager.ForgetTwoFactorClientAsync();
            StatusMessage = this.localizer[Lang.BrowserForgotten];
            return RedirectToPage();
        }
    }
}