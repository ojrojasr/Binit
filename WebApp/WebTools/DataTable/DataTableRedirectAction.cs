using Newtonsoft.Json;

namespace WebApp.WebTools.DataTable
{
    public class DataTableRedirectAction : DataTableAction
    {

        [JsonProperty(PropertyName = "target")]
        public string Target { get; set; }

        public DataTableRedirectAction()
        {
            this.TypeOfButton = "redirect";
            this.Target = "_self";
        }
    }
}