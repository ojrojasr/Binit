using Binit.Framework;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.WebTools.SweetAlert;

namespace WebApp.WebTools
{
    public class SweetAlert
    {
        // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
        // Tracking code: 3.0.0-issue1

        [JsonProperty(PropertyName = "typeOfAlert")]
        public string TypeOfAlert { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "confirmLabel")]
        public string ConfirmLabel { get; set; }

        [JsonProperty(PropertyName = "cancelLabel")]
        public string CancelLabel { get; set; }

        public SweetAlert(IStringLocalizer<SharedResources> localizer)
        {
            this.ConfirmLabel = localizer[Lang.ConfirmLabel];
            this.CancelLabel = localizer[Lang.CancelLabel];
        }

    }
}