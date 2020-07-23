using Binit.Framework;
using Binit.Framework.Interfaces.Email;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WebApp.Models;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.Profile;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IStringLocalizer<SharedResources> localizer;

        private readonly IFileManagerService fileManagerService;

        private readonly IUserService _userService;
        private readonly IAccountService _accountService;


        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IStringLocalizer<SharedResources> localizer,
            IFileManagerService fileManagerService,
            IUserService userService,
            IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            this.localizer = localizer;
            this.fileManagerService = fileManagerService;
            this._userService = userService;
            this._accountService = accountService;
        }

        [Display(Name = Lang.UsernameLabel)]
        public string Username { get; set; }

        [Display(Name = Lang.EmailLabel)]
        public string Email { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public FileViewModel Photo { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {


            [Required(ErrorMessage = Lang.NameRequired)]
            [StringLength(50, ErrorMessage = Lang.NameStringLength)]
            [Display(Name = Lang.NameLabel)]
            public string Name { get; set; }

            [Required(ErrorMessage = Lang.LastNameRequired)]
            [StringLength(50, ErrorMessage = Lang.LastNameStringLength)]
            [Display(Name = Lang.LastNameLabel)]
            public string LastName { get; set; }

            [Phone(ErrorMessage = Lang.PhoneNumberInvalid)]
            [Display(Name = Lang.PhoneNumberLabel)]
            public string PhoneNumber { get; set; }

            public Guid? PhotoId { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await this._accountService.GetUserFull();
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            Email = email;
            if (user.Photo != null)
            {
                Photo = new FileViewModel(user.Photo);
            }

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Name = user.Name,
                LastName = user.LastName,
                PhotoId = user.PhotoId
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            var email = await _userManager.GetEmailAsync(user);

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException(string.Format(this.localizer[Lang.ErrorSettingPhone], userId));
                }
            }

            user.PhotoId = Input.PhotoId;
            user.Name = Input.Name;
            user.LastName = Input.LastName;
            await this._userService.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = this.localizer[Lang.Updated];
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }


            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                email,
                this.localizer[Lang.VerificationEmailTitle],
                string.Format(this.localizer[Lang.VerificationEmailBody],
                              "<a href={1}>{2}</a>.",
                              HtmlEncoder.Default.Encode(callbackUrl),
                              this.localizer[Lang.ClickHere]));

            StatusMessage = this.localizer[Lang.EmailSent];
            return RedirectToPage();
        }
    }
}
