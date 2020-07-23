using Domain.Logic.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ViewComponents
{
    public class CardRowViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CardDTO model)
        {
            return View(model);
        }
    }
}