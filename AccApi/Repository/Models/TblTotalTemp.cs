using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblTotalTemp")]
    public partial class TblTotalTemp
    {
        [Key]
        public int Seq { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(2)]
        public string Div { get; set; }
        [Column("week")]
        public int? Week { get; set; }
        [StringLength(25)]
        public string Ref { get; set; }
        [StringLength(20)]
        public string Item { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public double? EstQty { get; set; }
        public double? Submitted { get; set; }
        [StringLength(10)]
        public string Unit { get; set; }
        public double? UnitRate { get; set; }
        [StringLength(100)]
        public string Sheet { get; set; }
        [Column("Qty_Prev")]
        public double? QtyPrev { get; set; }
        [Column("Qty_Cur")]
        public double? QtyCur { get; set; }
        [Column("Qty_Cum")]
        public double? QtyCum { get; set; }
        [Column("Per_Prev")]
        public float? PerPrev { get; set; }
        [Column("Per_Cur")]
        public float? PerCur { get; set; }
        [Column("Per_Cum")]
        public float? PerCum { get; set; }
        [Column("Amt_Prev")]
        public double? AmtPrev { get; set; }
        [Column("Amt_Cur")]
        public double? AmtCur { get; set; }
        [Column("Amt_Cum")]
        public double? AmtCum { get; set; }
        public byte? PaymentType { get; set; }
        [Column("ptPrefix")]
        [StringLength(10)]
        public string PtPrefix { get; set; }
        [Column("ptRowNumber")]
        public short? PtRowNumber { get; set; }
        public double? BudUnitRate { get; set; }
        public double? BudQty { get; set; }
    }
}
