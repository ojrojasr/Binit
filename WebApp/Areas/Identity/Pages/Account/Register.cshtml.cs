using Binit.Framework;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.Configuration;
using Binit.Framework.Interfaces.Email;
using Binit.Framework.Interfaces.ExceptionHandling;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ExceptionsLang = Binit.Framework.Localization.LocalizationConstants.BinitFramework.ExceptionHandling;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.Register;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IAccountService accountService;
        private readonly IEmailSender emailSender;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IRealmConfiguration realmConfiguration;
        private readonly IExceptionManager exceptionManager;
        private readonly IConfiguration configuration;

        public RegisterModel(IAccountService accountService, IEmailSender emailSender, IStringLocalizer<SharedResources> localizer,
            IRealmConfiguration realmConfiguration, IExceptionManager exceptionManager, IConfiguration configuration)
        {
            this.accountService = accountService;
            this.emailSender = emailSender;
            this.localizer = localizer;
            this.configuration = configuration;
            this.realmConfiguration = realmConfiguration;
            this.exceptionManager = exceptionManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = Lang.EmailRequired)]
            [EmailAddress(ErrorMessage = Lang.EmailInvalid)]
            [Display(Name = Lang.EmailLabel)]
            public string Email { get; set; }

            [Required(ErrorMessage = Lang.NameRequired)]
            [StringLength(50, ErrorMessage = Lang.NameStringLength)]
            [Display(Name = Lang.NameLabel)]
            public string Name { get; set; }

            [Required(ErrorMessage = Lang.LastNameRequired)]
            [StringLength(50, ErrorMessage = Lang.LastNameStringLength)]
            [Display(Name = Lang.LastNameLabel)]
            public string LastName { get; set; }

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

        public void OnGet(string returnUrl = null)
        {
            // If realm doesn't allow self sign up, throw NotFound exception.
            if (!realmConfiguration.AllowSelfSignUp)
                throw this.exceptionManager.Handle(new NotFoundException(localizer[ExceptionsLang.ResourceNotFoundGenericEx]));

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Login");

            if (ModelState.IsValid)
            {
                var user = new BackOfficeUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    LastName = Input.LastName
                };

                try
                {
                    var code = await accountService.Register(user, Input.Password);

                    var callbackUrl = Url.Page(
                                            "/Account/ConfirmEmail",
                                            pageHandler: null,
                                            values: new { userId = user.Id, code = code },
                                            protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(user.Email, localizer[Lang.ConfirmationEmailTitle], new WelcomeDTO(configuration, localizer) { Name = user.Email, CallbackUrl = callbackUrl });

                    return RedirectToPage("./RegisterConfirmation");
                }
                catch (IdentityException idex)
                {
                    foreach (var error in idex.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (Binit.Framework.ExceptionHandling.Types.ValidationException vex)
                {
                    ModelState.AddModelError(string.Empty, vex.Message);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
