using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class AssignRevisionDetails
    {
        public int resourceID { get; set; }
        public string resourceName { get; set; }
        public string resourceUnit { get; set; }
        public double? resourceQty { get; set; }
        public double? price { get; set; }

        public double? assignpercent { get; set; }
        public double? assignQty { get; set; }
        public double? assignPrice { get; set; }
        public int revisionId { get; set; }
        public double? priceOrigCurrency { get; set; }

    }

    public class SupplierPercent
    {
        public int supID { get; set; }
        public double percent { get; set; }
    }

    public class SupplierResrouces
    {
        public int resourceID { get; set; }
        public List<SupplierPercent> supplierPercents { get; set; }
    }
}
