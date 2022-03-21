using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Models
{
    public class DatatablesModel
    {
        public partial class DataTablesRequestParameter
        {
            public int draw { get; set; }
            public int start { get; set; }
            public int length { get; set; }
            public DataTablesRequestSearch search { get; set; }
            public List<DataTablesRequestOrder> order { get; set; }
            public List<DataTablesRequestColumn> columns { get; set; }
            public string SortOrder
            {
                get
                {
                    List<string> _orderList = new List<string>();
                    foreach (var orderItem in order)
                    {
                        _orderList.Add((columns != null && orderItem != null ? columns[orderItem.column].data + (orderItem.dir == eOrderDirection.DESC ? " " + orderItem.dir : String.Empty) : null));
                    }
                    return string.Join(",", _orderList);
                }
            }

            public partial class DataTablesRequestSearch
            {
                public string value { get; set; }
                public bool regex { get; set; }
            }

            public partial class DataTablesRequestOrder
            {
                public int column { get; set; }
                public eOrderDirection dir { get; set; }
            }

            public partial class DataTablesRequestColumn
            {
                public string data { get; set; }
                public string name { get; set; }
                public bool searchable { get; set; }
                public bool orderable { get; set; }
                public DataTablesRequestSearch search { get; set; }
                public bool IsEquality { get; set; }
            }

            public enum eOrderDirection
            {
                ASC,
                DESC
            }

        }

        public partial class DataTablesResponseParameter<T>
        {
            public int draw { get; set; }
            public int recordsTotal { get; set; }
            public int recordsFiltered { get; set; }
            public IEnumerable<T> data { get; set; }
        }
    }
}
