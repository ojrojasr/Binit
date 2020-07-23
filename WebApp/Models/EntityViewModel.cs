using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.EntityViewModel;

namespace WebApp.Models
{
    public class EntityViewModel
    {
        [Display(Name = Lang.IdLabel)]
        public string Id { get; set; }
    }
}