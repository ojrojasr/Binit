using System.Collections.Generic;

namespace WebApp.WebTools.DataTable
{
    public class DataTableRequest
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public int page
        {
            get
            {
                return length == 0 ? 1 : (start / length) + 1;
            }
        }
        public SearchDataTable search { get; set; }
        public List<OrderDataTable> order { get; set; }
        public List<ColumnDataTable> columns { get; set; }
    }

    public class OrderDataTable
    {
        public string dir { get; set; }
        public int column { get; set; }
    }

    public class SearchDataTable
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class ColumnDataTable
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchDataTable search { get; set; }
    }
}