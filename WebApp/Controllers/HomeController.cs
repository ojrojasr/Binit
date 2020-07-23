using Binit.Framework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.HomeController;

namespace WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer<SharedResources> localizer;

        public HomeController(IStringLocalizer<SharedResources> localizer)
        {
            this.localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = this.localizer[Lang.IndexTitle];

            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["Title"] = this.localizer[Lang.PrivacyTitle];

            return View();
        }
    }
}
