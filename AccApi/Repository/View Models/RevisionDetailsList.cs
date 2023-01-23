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
        public int RdResourceSeq { get; set; }
        //Supplier price after exchange 
        public double? RdPrice { get; set; }
        public byte? RdMissedPrice { get; set; }

        public string RdBoqItem { get; set; }
        public string RdBoqItemDescription { get; set; }
        public string RdItemDescription  { get; set; }

        //budget
        public double? RdQty { get; set; }
        public double? RdUnitRate { get; set; }
        public double? RdTotalBudget { get; set; }
        ///budget

        public double? ExchangeRate{ get; set; }

        //Supplier price before exchange 
        public double? RdOriginalPrice { get; set; }

        //Total Supplier price after exchange 
        public double? TotalSupplierPrice { get; set; }
        //currency of supplier
        public string currency { get; set; }
        public string RdMissedPriceReason { get; set; }

        public double? RdDiscount { get; set; }
        public double? RdPriceAfterDiscount { get; set; }
        public double? RdTotalPrice { get; set; }
        public byte? RdAddedItem { get; set; }
        public string InsertedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? RdAddedItemOn { get; set; }

    }
}
