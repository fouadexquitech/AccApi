using System.Collections.Generic;

namespace AccApi.Repository.View_Models.Common
{
    public class DataTablesResponse<T>
    {
        public List<T>? Data { get; set; }

        public int Draw { get; set; }

        public int RecordsFiltered { get; set; }

        public int RecordsTotal { get; set; }

        public double? FinalTotalPrice { get; set; }

        public double? FinalUnitPrice { get; set; }
    }
}
