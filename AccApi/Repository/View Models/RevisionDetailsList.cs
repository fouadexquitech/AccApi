using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class RevisionDetailsList
    {
        public int RdRevisionId { get; set; }
        public string RdResourceSeq { get; set; }
        //Supplier price after exchange 
        public double? RdPrice { get; set; }
        public byte? RdMissedPrice { get; set; }

        public string RdBoqItem { get; set; }
        public string RdBoqItemDescription { get; set; }
        public string RdItemDescription  { get; set; }

        //budget
        public double? RdQty { get; set; }  //Supplier Qty
        public double? RdQuotationQty { get; set; } //ACC Qty
        public double? RdUnitRate { get; set; }
        public double? RdTotalBudget { get; set; }
        ///budget

        public double? ExchangeRate{ get; set; }

        //Supplier price before exchange 
        public double? RdOriginalPrice { get; set; }

        //Total Supplier price after exchange 
        public double? TotalSupplierPrice { get; set; }
        //currency of supplier
        public string? currency { get; set; }
        public string RdMissedPriceReason { get; set; }

        public double? RdDiscount { get; set; }
        public double? RdPriceAfterDiscount { get; set; }
        public double? RdTotalPrice { get; set; }
        public byte? RdAddedItem { get; set; }
        public string? InsertedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? RdAddedItemOn { get; set; }

        public bool? IsAlternative { get; set; }
        public bool? IsNew { get; set; }
        public int? NewItemId { get; set; }
        public int? NewItemResourceId { get; set; }
        public string? ParentItemO { get; set; }
        public int? ParentResourceId { get; set; }

        public string? L1 { get; set; }
        public string? L2 { get; set; }
        public string? L3 { get; set; }
        public string? L4 { get; set; }
        public string? L5 { get; set; }
        public string? L6 { get; set; }
        public string? C1 { get; set; }
        public string? C2 { get; set; }
        public string? C3 { get; set; }
        public string? C4 { get; set; }
        public string? C5 { get; set; }
        public string? C6 { get; set; }
        public string? LevelName { get; set; }
        public string? Unit { get; set; }
        public string? Comments { get; set; }
    }

    public class LevelModel
    {
        public string? LevelName { get; set; }
        public List<RevisionDetailsList> Items { get; set; }
    }
}
