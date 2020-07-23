using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.ConfirmEmail;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IStringLocalizer<SharedResources> localizer;


        public ConfirmEmailModel(IAccountService accountService, IStringLocalizer<SharedResources> localizer)
        {
            this.accountService = accountService;
            this.localizer = localizer;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            try
            {
                await this.accountService.ConfirmEmail(userId, code);

                return Page();
            }
            catch (NotFoundException)
            {
                // Don't reveal that the user does not exist
                return Page();
            }
            catch (IdentityException)
            {
                throw new InvalidOperationException(string.Format(localizer[Lang.InvalidOperationException], userId));
            }
        }
    }
}
