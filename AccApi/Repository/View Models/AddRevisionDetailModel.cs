using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccApi.Repository.View_Models
{
    public class AddRevisionDetailModel
    {
        public int? BoqResourceSeq { get; set; }
        public string? ResourceDescription { get; set; }
        public string? ItemO { get; set; }
        public string? ItemDescription { get; set; }
        public double? Quantity { get; set; }
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
        public int? ParentResourceId { get; set; }
        public double? UnitPriceAfterDiscount { get; set; }=0;
    }

    public class AddCondModel
    {
        public int Id { get; set; }
        public string? CondValue { get; set; }
        public string? ACCCondValue { get; set; }
        public string? ProjectCode { get; set; }
    }
}
