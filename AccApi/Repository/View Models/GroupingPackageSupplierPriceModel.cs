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

        public int? RevisionId { get; set; }
        public DateTime? LastRevisionDate { get; set; }
        public double? TotalPrice { get; set; }

        public double? OriginalCurrencyPrice { get; set; }

        public byte? MissedPrice { get; set; }

        public int? GroupId { get; set; }

        public double? AssignedPercentage { get; set; }

        public double? AssignedQty { get; set; }

        public string? OriginalCurrency { get; set; }
        public double? ExchRate { get; set; }
        public double? ExchRateNow { get; set; }
        public double? Discount { get; set; }
        public double? UPriceAfterDiscount { get; set; }=0;

        public byte byBoq { get; set; }

        public bool? IsAlternative { get; set; }
        public bool? IsNewItem { get; set; }
        public int? NewItemId { get; set; }
        public int? NewItemResourceId { get; set; }
        public string? ParentItemO { get; set; }
        public int? ParentResourceId { get; set; }
        public bool isCreatedByThisSupplier { get; set; } = false;
        public bool? IsExcluded { get; set; } = false;

        public string ItemDescription { get; set; }
        public string ResourceDescription { get; set; }
    }

    public class LiveExchange
    {
        public string? fromCurrency { get; set; }
        public double? ExchRateNow { get; set; }
    }
}
