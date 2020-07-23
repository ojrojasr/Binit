using Newtonsoft.Json;

namespace WebApp.WebTools.DataTable
{
    public class DataTableAction
    {
        [JsonProperty(PropertyName = "internalName")]
        public string InternalName { get; set; }

        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }

        [JsonProperty(PropertyName = "icon")]
        public string Icon { get; set; }

        [JsonProperty(PropertyName = "class")]
        public string Class { get; set; }

        [JsonProperty(PropertyName = "typeOfButton")]
        public string TypeOfButton { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        // GET - POST - PUT
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "successTitle")]
        public string SuccessTitle { get; set; }

        public DataTableAction()
        {
            this.DisplayName = string.Empty;
            this.TypeOfButton = "action";
        }
    }
}