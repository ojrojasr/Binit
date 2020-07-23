using Newtonsoft.Json;
using System.Collections.Generic;

namespace WebApp.WebTools.DataTable
{
    public class DataTableRow
    {
        // We'll temporarily use Newtonsoft for json serialization until mechanism to handle circular references is fixed for System.Text.Json.Serialization
        // Tracking code: 3.0.0-issue1
        [JsonProperty(PropertyName = "id")]
        public string DT_RowId { get; set; }

        public List<DataTableAction> Actions { get; set; }
    }
}