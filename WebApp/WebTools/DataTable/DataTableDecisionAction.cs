using Newtonsoft.Json;

namespace WebApp.WebTools.DataTable
{
    public class DataTableDecisionAction : DataTableAction
    {

        [JsonProperty(PropertyName = "modal")]
        public SweetAlert Modal { get; set; }

        public DataTableDecisionAction()
        {
            this.TypeOfButton = "decision";
        }

    }
}