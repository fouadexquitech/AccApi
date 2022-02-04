using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class PackageSuppliersPrice
    {
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public List<RevisionDetails> revisionDetails { get; set; }
        public List<FieldList> fieldLists { get; set; }
        public decimal totalprice { get; set; }

        public decimal totalAdditionalPrice { get; set; }

        public decimal totalNetPrice { get; set; }
    }

    public class FieldList
    {
        public string Label { get; set; }
        public int Value { get; set; }
    }

    public class RevisionDetails
    {
        public int resourceID { get; set; }
        public string resourceName { get; set; }
        public string resourceUnit { get; set; }
        public double? resourceQty { get; set; }
        public double? price { get; set; }
        public double? perc { get; set; }
    }
}
