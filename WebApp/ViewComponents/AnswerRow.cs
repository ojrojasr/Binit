using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.ViewComponents
{
    public class AnswerRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int index, QuestionViewModel model, bool editable = true)
        {
            ViewData["Index"] = index;
            ViewData["Editable"] = editable;
            return View(model);
        }
    }
}