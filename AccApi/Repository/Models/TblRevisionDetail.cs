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
        [StringLength(25)]
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
        [StringLength(25)]
        public string ParentItemO { get; set; }
        [Column("parentResourceId")]
        public int? ParentResourceId { get; set; }
    }
}
