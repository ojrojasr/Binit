using Binit.Framework;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.DisableTwoFA;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class Disable2faModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<Disable2faModel> _logger;
        private readonly IStringLocalizer<SharedResources> localizer;


        public Disable2faModel(
            UserManager<ApplicationUser> userManager,
            ILogger<Disable2faModel> logger,
            IStringLocalizer<SharedResources> localizer)
        {
            _userManager = userManager;
            _logger = logger;
            this.localizer = localizer;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user))
            {
                throw new InvalidOperationException(string.Format(this.localizer[Lang.CannotDisable2FA], _userManager.GetUserId(User)));
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false);
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException(string.Format(this.localizer[Lang.InvalidOperationException], _userManager.GetUserId(User)));
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(User));
            StatusMessage = this.localizer[Lang.TwoFADisabled];
            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}