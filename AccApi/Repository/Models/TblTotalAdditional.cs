using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblTotalAdditional")]
    public partial class TblTotalAdditional
    {
        [Key]
        [Column("taSeq")]
        [StringLength(14)]
        public string TaSeq { get; set; }
        [Required]
        [StringLength(10)]
        public string Project { get; set; }
        [Column("week")]
        public int Week { get; set; }
        [Column("cc")]
        [StringLength(15)]
        public string Cc { get; set; }
        public int? Amount { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column("taType")]
        public byte? TaType { get; set; }
        [Column("taLab", TypeName = "money")]
        public decimal? TaLab { get; set; }
        [Column("taMAt", TypeName = "money")]
        public decimal? TaMat { get; set; }
        [Column("taSubC", TypeName = "money")]
        public decimal? TaSubC { get; set; }
        [Column("taEqp", TypeName = "money")]
        public decimal? TaEqp { get; set; }
        [Column("taDiv")]
        [StringLength(2)]
        public string TaDiv { get; set; }
        [Column("taSubDiv")]
        [StringLength(3)]
        public string TaSubDiv { get; set; }
        [Column("taAbv")]
        [StringLength(3)]
        public string TaAbv { get; set; }
        [Column("taSkip")]
        public bool? TaSkip { get; set; }
        [Column("taOthers", TypeName = "money")]
        public decimal? TaOthers { get; set; }
        [Column("taPhase")]
        public int? TaPhase { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("taTrade")]
        [StringLength(5)]
        public string TaTrade { get; set; }
        [Column("taSkipTotPayment")]
        public int? TaSkipTotPayment { get; set; }
        [Column("taAddTotBudget")]
        public bool? TaAddTotBudget { get; set; }
        [Column("taVORefId")]
        public int? TaVorefId { get; set; }
        [Column(TypeName = "money")]
        public decimal? Submitted { get; set; }
        [Column("SubmittedAmt_Cum", TypeName = "money")]
        public decimal? SubmittedAmtCum { get; set; }
        [Column("SubmittedAmt_Prev", TypeName = "money")]
        public decimal? SubmittedAmtPrev { get; set; }
        [Column("taPer")]
        public double? TaPer { get; set; }
        [Column("CertifiedAmt_Cum", TypeName = "money")]
        public decimal? CertifiedAmtCum { get; set; }
        [Column(TypeName = "money")]
        public decimal? CertifiedAmount { get; set; }
        [Column("CertifiedAmt_Prev", TypeName = "money")]
        public decimal? CertifiedAmtPrev { get; set; }
        [Column(TypeName = "money")]
        public decimal? CertifiedPer { get; set; }
        [Column(TypeName = "money")]
        public decimal? CertifiedQty { get; set; }
        [Column("CertifiedPer_Cum", TypeName = "money")]
        public decimal? CertifiedPerCum { get; set; }
        [Column("CertifiedQty_Cum", TypeName = "money")]
        public decimal? CertifiedQtyCum { get; set; }
        [Column("CertifiedPer_Prev", TypeName = "money")]
        public decimal? CertifiedPerPrev { get; set; }
        [Column("CertifiedQty_Prev", TypeName = "money")]
        public decimal? CertifiedQtyPrev { get; set; }
        [Column("taVORef")]
        [StringLength(500)]
        public string TaVoref { get; set; }
        [Column("SubmittedPer_Prev")]
        public double? SubmittedPerPrev { get; set; }
        public double? Per { get; set; }
        [Column("SubmittedPer_Cum")]
        public double? SubmittedPerCum { get; set; }
        [Column("paymentNo")]
        public int? PaymentNo { get; set; }
        [Column("status")]
        public byte? Status { get; set; }
    }
}
