using System.Collections.Generic;

namespace WebApp.WebTools.DataTable
{
    public class DataTableMultivalueColumn
    {
        public DataTableTagStyle DefaultStyle { get; set; }
        public List<DataTableTagStylePerValue> StylePerValue { get; set; }
        public List<string> Data { get; set; }
        public int Limit { get; set; }

        public DataTableMultivalueColumn(List<string> data)
        {
            this.Data = data;
            this.DefaultStyle = DataTableTagStyle.Blue;
            this.Limit = 0;
        }
    }
}