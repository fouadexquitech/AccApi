using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblFormwork")]
    public partial class TblFormwork
    {
        [Key]
        [Column("fwSeq")]
        [StringLength(14)]
        public string FwSeq { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(15)]
        public string CostCode { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        public double? PurchasedValue { get; set; }
        public double? ResaleValue { get; set; }
        public float? TtlAreaCovered { get; set; }
        [Column(TypeName = "ntext")]
        public string Remark { get; set; }
        public float? UnitCost { get; set; }
        [Column("fwDiv")]
        [StringLength(2)]
        public string FwDiv { get; set; }
        [Column("fwSubDiv")]
        [StringLength(3)]
        public string FwSubDiv { get; set; }
        [Column("fwTrade")]
        [StringLength(5)]
        public string FwTrade { get; set; }
        [Column("fwSubTrade")]
        [StringLength(3)]
        public string FwSubTrade { get; set; }
        [Column("fwPhase")]
        public int? FwPhase { get; set; }
        [Column("WBSlevel")]
        [StringLength(5)]
        public string Wbslevel { get; set; }
    }
}
