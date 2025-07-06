using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccApi.Repository.View_Models
{
    public class AddRevisionDetailModel
    {
        public string? BoqResourceSeq { get; set; }
        public string? ResourceDescription { get; set; }
        public string? ItemO { get; set; }
        public string? ItemDescription { get; set; }
        public double? Quantity { get; set; }
        public double? QuotationQty { get; set; }
        public double? UnitPrice { get; set; } = 0;
        public double? TotalPrice { get; set; } = 0;
        public double? DiscountPerc { get; set; } = 0;
        public string? Comments { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsSynched { get; set; } = false;
        public string? ProjectCode { get; set; }
        public bool? IsAlternative { get; set; }
        public bool? IsNewItem { get; set; }
        public int? NewItemId { get; set; }
        public int? NewItemResourceId { get; set; }
        public string? ParentItemO { get; set; }
        public string? ParentResourceId { get; set; }
        public double? UnitPriceAfterDiscount { get; set; }=0;

        public string UnitO { get; set; } = "";
        public string BoqCtg { get; set; } = "";
        public string BoqUnitMesure { get; set; } = "";
        public string L1 { get; set; } = "";
        public string L2 { get; set; } = "";
        public string L3 { get; set; } = "";
        public string L4 { get; set; } = "";
        public string L5 { get; set; } = "";
        public string L6 { get; set; } = "";
        public string L7 { get; set; } = "";
        public string L8 { get; set; } = "";
        public string L9 { get; set; } = "";
        public string L10 { get; set; } = "";
        public string C1 { get; set; } = "";
        public string C2 { get; set; } = "";
        public string C3 { get; set; } = "";
        public string C4 { get; set; } = "";
        public string C5 { get; set; } = "";
        public string C6 { get; set; } = "";
        public string C7 { get; set; } = "";
        public string C8 { get; set; } = "";
        public string C9 { get; set; } = "";
        public string C10 { get; set; } = "";
        public string C11 { get; set; } = "";
        public string C12 { get; set; } = "";
        public string C13 { get; set; } = "";
        public string C14 { get; set; } = "";
        public string C15 { get; set; } = "";
    }

    public class AddCondModel
    {
        public int Id { get; set; }
        public string? CondValue { get; set; }
        public string? ACCCondValue { get; set; }
        public string? ProjectCode { get; set; }
    }
}
