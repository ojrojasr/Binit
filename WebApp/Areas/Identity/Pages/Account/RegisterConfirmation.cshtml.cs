using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmation : PageModel
    {
        public void OnGet()
        {
        }
    }
}
