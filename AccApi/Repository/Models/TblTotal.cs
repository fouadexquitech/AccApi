using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblTotal")]
    public partial class TblTotal
    {
        [Key]
        [StringLength(10)]
        public string Project { get; set; }
        [Key]
        [Column("week")]
        public int Week { get; set; }
        [Key]
        [StringLength(50)]
        public string Item { get; set; }
        public double? Qty { get; set; }
        public float? Per { get; set; }
        public double? Submitted { get; set; }
        public double? CertifiedQty { get; set; }
        public float? CertifiedPer { get; set; }
        public float? SellingPrice { get; set; }
        [StringLength(50)]
        public string Div { get; set; }
        [Column("SubmittedQty_Cum")]
        public double? SubmittedQtyCum { get; set; }
        [Column("SubmittedPer_Cum")]
        public float? SubmittedPerCum { get; set; }
        [Column("CertifiedQty_Cum")]
        public double? CertifiedQtyCum { get; set; }
        [Column("CertifiedPer_Cum")]
        public float? CertifiedPerCum { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        public double? EstQty { get; set; }
        public double? UnitRate { get; set; }
        public double? Amt { get; set; }
        [Column("SubmittedPer_Prev")]
        public float? SubmittedPerPrev { get; set; }
        [Column("SubmittedQty_Prev")]
        public double? SubmittedQtyPrev { get; set; }
        [Column("SubmittedAmt_Prev")]
        public double? SubmittedAmtPrev { get; set; }
        public double? CertifiedAmt { get; set; }
        [Column("CertifiedAmt_Cum")]
        public double? CertifiedAmtCum { get; set; }
        [Column("SubmittedAmt_Cum")]
        public double? SubmittedAmtCum { get; set; }
        [Column("CertifiedPer_Prev")]
        public float? CertifiedPerPrev { get; set; }
        [Column("CertifiedQty_Prev")]
        public double? CertifiedQtyPrev { get; set; }
        [Column("CertifiedAmt_Prev")]
        public double? CertifiedAmtPrev { get; set; }
        [StringLength(100)]
        public string Sheet { get; set; }
        [StringLength(50)]
        public string Ref { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public double? BudUnitRate { get; set; }
        public double? BudQty { get; set; }
        public double? CostPlusRate { get; set; }
        [Column(TypeName = "money")]
        public decimal? CostPlusAmount { get; set; }
        [Column("billingNo")]
        [StringLength(500)]
        public string BillingNo { get; set; }
    }
}
