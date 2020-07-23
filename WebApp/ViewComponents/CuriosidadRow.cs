using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.ViewComponents
{
    public class CuriosidadRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int index, PeliculaViewModel model, bool editable = true)
        {
            ViewData["Index"] = index;
            ViewData["Editable"] = editable;
            return View(model);
        }
    }
}