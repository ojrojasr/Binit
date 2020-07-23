using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class CreatePasswordConfirmation : PageModel
    {
        public CreatePasswordConfirmation()
        {

        }

        public void OnGet(string userId, string code)
        {
        }
    }
}
