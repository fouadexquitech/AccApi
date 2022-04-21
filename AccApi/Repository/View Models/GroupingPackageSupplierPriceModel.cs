using System;

namespace AccApi.Repository.View_Models
{
    public class GroupingPackageSupplierPriceModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int? BoqResourceId { get; set; }

        public string? BoqItemO { get; set; }

        public double? UnitPrice { get; set; }

        public double? Qty { get; set; }

        public DateTime? LastRevisionDate { get; set; }
        public double? TotalPrice { get; set; }

        public double? OriginalCurrencyPrice { get; set; }

        public byte? MissedPrice { get; set; }

        public int? GroupId { get; set; }

        public double? AssignedPercentage { get; set; }

        public double? AssignedQty { get; set; }
    }
}
