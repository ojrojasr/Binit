using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.ViewComponents
{
    public class FeatureRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int index, ProductViewModel model, bool editable = true)
        {
            ViewData["Index"] = index;
            ViewData["Editable"] = editable;
            return View(model);
        }
    }
}