using Binit.Framework;
using Domain.Entities.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Areas.Identity.Pages.Account.DownloadYourData;

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly IStringLocalizer<SharedResources> localizer;


        public DownloadPersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ILogger<DownloadPersonalDataModel> logger,
            IStringLocalizer<SharedResources> localizer)
        {
            _userManager = userManager;
            _logger = logger;
            this.localizer = localizer;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(string.Format(this.localizer[Lang.CannotLoadUser], _userManager.GetUserId(User)));
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");

            // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
            // Tracking code: 3.0.0-issue1
            return new FileContentResult(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(personalData)), "text/json");
        }
    }
}
