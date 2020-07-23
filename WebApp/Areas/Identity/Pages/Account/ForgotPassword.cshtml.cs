using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.Email;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.ForgotPassword;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IConfiguration configuration;

        public ForgotPasswordModel(IAccountService accountService, IEmailSender emailSender, IStringLocalizer<SharedResources> localizer, IConfiguration configuration)
        {
            this.accountService = accountService;
            this.emailSender = emailSender;
            this.localizer = localizer;
            this.configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.EmailRequired)]
            [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
            [Display(Name = Lang.EmailLabel)]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var code = await accountService.GeneratePasswordToken(Input.Email);

                    var callbackUrl = Url.Page(
                        "/Account/ResetPassword",
                        pageHandler: null,
                        values: new { code },
                        protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input.Email, localizer[Lang.SendEmailTitle], new ForgotPasswordDTO(configuration, localizer) { Name = Input.Email, CallbackUrl = callbackUrl });

                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
                catch (NotFoundException)
                {
                    // Don't reveal that the user does not exist or is not confirmed.
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }
            }

            return Page();
        }
    }
}
