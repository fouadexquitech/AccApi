using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblVODtl")]
    public partial class TblVodtl
    {
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [Column("seqHdr")]
        public int SeqHdr { get; set; }
        [Key]
        [StringLength(20)]
        public string Item { get; set; }
        public string Description { get; set; }
        [StringLength(10)]
        public string Unit { get; set; }
        public double? BudUnitRate { get; set; }
        public double? BudQty { get; set; }
        public double? Submitted { get; set; }
        public double? AddQty { get; set; }
        public double? AddAmt { get; set; }
        public double? OmitQty { get; set; }
        public double? OmitAmt { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("insertDate", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        public float? Per { get; set; }
        public double? CertifiedQty { get; set; }
        public float? CertifiedPer { get; set; }
        public float? SellingPrice { get; set; }
        [StringLength(2)]
        public string Div { get; set; }
        [Column("SubmittedQty_Cum")]
        public double? SubmittedQtyCum { get; set; }
        [Column("SubmittedPer_Cum")]
        public float? SubmittedPerCum { get; set; }
        [Column("CertifiedQty_Cum")]
        public double? CertifiedQtyCum { get; set; }
        [Column("CertifiedPer_Cum")]
        public float? CertifiedPerCum { get; set; }
        public double? EstQty { get; set; }
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
        [StringLength(25)]
        public string Ref { get; set; }
        public double? CostPlusRate { get; set; }
        [Column(TypeName = "money")]
        public decimal? CostPlusAmount { get; set; }
        [Column("billingNo")]
        [StringLength(500)]
        public string BillingNo { get; set; }
        public int? Responsible { get; set; }
        public double? OmitQtyL { get; set; }
        public double? OmitUnitRateL { get; set; }
        public double? OmitAmtL { get; set; }
        public double? OmitQtyM { get; set; }
        public double? OmitUnitRateM { get; set; }
        public double? OmitAmtM { get; set; }
        public double? OmitQtyS { get; set; }
        public double? OmitUnitRateS { get; set; }
        public double? OmitAmtS { get; set; }
        public double? OmitQtyE { get; set; }
        public double? OmitUnitRateE { get; set; }
        public double? OmitAmtE { get; set; }
        public double? OmitQtyOh { get; set; }
        public double? OmitUnitRateOh { get; set; }
        public double? OmitAmtOh { get; set; }
        [Column("AddQty_Cert", TypeName = "money")]
        public decimal? AddQtyCert { get; set; }
        [Column("AddAmt_Cert", TypeName = "money")]
        public decimal? AddAmtCert { get; set; }
        [Column("OmitQty_Cert", TypeName = "money")]
        public decimal? OmitQtyCert { get; set; }
        [Column("OmitAmt_Cert", TypeName = "money")]
        public decimal? OmitAmtCert { get; set; }
        [Column("itemType")]
        public int? ItemType { get; set; }
        [StringLength(100)]
        public string BoqClass { get; set; }
        [Column("notes", TypeName = "ntext")]
        public string Notes { get; set; }
        [Column("subcAmount", TypeName = "money")]
        public decimal? SubcAmount { get; set; }
        [Column("subcAmount_Cert", TypeName = "money")]
        public decimal? SubcAmountCert { get; set; }
        [Column("risk")]
        public int? Risk { get; set; }
        [Column("realizablPerc")]
        public double? RealizablPerc { get; set; }
        [Column("addOmit")]
        public byte? AddOmit { get; set; }
        [Column(TypeName = "money")]
        public decimal? EmployerAmt { get; set; }
        [Column("realizablAmt", TypeName = "money")]
        public decimal? RealizablAmt { get; set; }
        [Column("realizablSubcAmt", TypeName = "money")]
        public decimal? RealizablSubcAmt { get; set; }
        public double? OmitQtyOth { get; set; }
        public double? OmitUnitRateOth { get; set; }
        public double? OmitAmtOth { get; set; }
        public double? ConsUnitRate { get; set; }
        public double? EmployerUnitRate { get; set; }
        public int? VoItemStatus { get; set; }
        [Column("finalApprovalPerc")]
        public double? FinalApprovalPerc { get; set; }
        [Column("finalApprovalAmt")]
        public double? FinalApprovalAmt { get; set; }
        [Column("finalApprovalSubcAmt")]
        public double? FinalApprovalSubcAmt { get; set; }
        public double? EmployerCertifiedQty { get; set; }
        [Column("EmployerSubcAmount_Cert")]
        public double? EmployerSubcAmountCert { get; set; }

        [ForeignKey(nameof(SeqHdr))]
        [InverseProperty(nameof(TblVohdr.TblVodtls))]
        public virtual TblVohdr SeqHdrNavigation { get; set; }
    }
}
