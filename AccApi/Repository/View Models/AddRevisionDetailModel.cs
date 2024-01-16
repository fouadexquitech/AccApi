using System;
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
        public string ParentItemO { get; set; }
        public int? ParentResourceId { get; set; }
    }
}
