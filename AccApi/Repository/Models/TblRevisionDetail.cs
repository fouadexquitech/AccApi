using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblRevisionDetails")]
    public partial class TblRevisionDetail
    {
        [Key]
        [Column("rdRevisionId")]
        public int RdRevisionId { get; set; }
        [Key]
        [Column("rdResourceSeq")]
        public int RdResourceSeq { get; set; }
        [Key]
        [Column("rdBoqItem")]
        [StringLength(100)]
        public string RdBoqItem { get; set; }
        [Column("rdPrice")]
        public double? RdPrice { get; set; }
        [Column("rdQty")]
        public double? RdQty { get; set; }
        [Column("rdComment", TypeName = "text")]
        public string RdComment { get; set; }
        [Column("rdAssignedPerc")]
        public double? RdAssignedPerc { get; set; }
        [Column("rdAssignedQty")]
        public double? RdAssignedQty { get; set; }
        [Column("rdAssignedPrice")]
        public double? RdAssignedPrice { get; set; }
        [Column("rdPriceOrigCurrency")]
        public double? RdPriceOrigCurrency { get; set; }
        [Column("rdMissedPrice")]
        public byte? RdMissedPrice { get; set; }
        [Column("rdMissedPriceReason")]
        public string RdMissedPriceReason { get; set; }
        [Column("rdAddedItem")]
        public byte? RdAddedItem { get; set; }
        [Column("insertedBy")]
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("rdAddedItemOn", TypeName = "datetime")]
        public DateTime? RdAddedItemOn { get; set; }
        [Column("rdDiscount")]
        public double? RdDiscount { get; set; }
        public bool? IsSynched { get; set; }
        public bool? IsAlternative { get; set; }
        public bool? IsNew { get; set; }
        public int? NewItemId { get; set; }
        public int? NewItemResourceId { get; set; }
        [Column("parentItemO")]
        [StringLength(100)]
        public string ParentItemO { get; set; }
        [Column("parentResourceId")]
        public int? ParentResourceId { get; set; }
        public string ResourceDescription { get; set; }
        public string ItemDescription { get; set; }
        [Column("rdQuotationQty")]
        public double? RdQuotationQty { get; set; }
        public int? ItemCopiedFromRevision { get; set; }
        public double? TotalPrice { get; set; }
        public double? UnitPriceAfterDiscount { get; set; }
        [Column("isExcluded")]
        public bool? IsExcluded { get; set; }
        [Column("insertDate", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [StringLength(255)]
        public string UnitO { get; set; }
        [StringLength(10)]
        public string BoqCtg { get; set; }
        [StringLength(10)]
        public string BoqUnitMesure { get; set; }
        public string C1 { get; set; }
        public string C10 { get; set; }
        public string C11 { get; set; }
        public string C12 { get; set; }
        public string C13 { get; set; }
        public string C14 { get; set; }
        public string C15 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }
        public string C4 { get; set; }
        public string C5 { get; set; }
        public string C6 { get; set; }
        public string C7 { get; set; }
        public string C8 { get; set; }
        public string C9 { get; set; }
        public string L1 { get; set; }
        public string L10 { get; set; }
        public string L2 { get; set; }
        public string L3 { get; set; }
        public string L4 { get; set; }
        public string L5 { get; set; }
        public string L6 { get; set; }
        public string L7 { get; set; }
        public string L8 { get; set; }
        public string L9 { get; set; }
    }
}
