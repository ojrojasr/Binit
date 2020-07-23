using Binit.Framework;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.GenerateRecoveryCodes;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class GenerateRecoveryCodesModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<GenerateRecoveryCodesModel> _logger;
        private readonly IStringLocalizer<SharedResources> localizer;


        public GenerateRecoveryCodesModel(
            UserManager<ApplicationUser> userManager,
            ILogger<GenerateRecoveryCodesModel> logger,
            IStringLocalizer<SharedResources> localizer)
        {
            _userManager = userManager;
            _logger = logger;
            this.localizer = localizer;
        }

        [TempData]
        public string[] RecoveryCodes { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            if (!isTwoFactorEnabled)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException(string.Format(this.localizer[Lang.ErrorTwoFADisabled], userId));
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

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException(string.Format(this.localizer[Lang.ErrorTwoFADisabled], userId));
            }

            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
            RecoveryCodes = recoveryCodes.ToArray();

            _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);
            StatusMessage = this.localizer[Lang.NewCodesGenerated];
            return RedirectToPage("./ShowRecoveryCodes");
        }
    }
}