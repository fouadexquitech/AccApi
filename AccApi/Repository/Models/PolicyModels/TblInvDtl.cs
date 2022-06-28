using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInvDtl")]
    public partial class TblInvDtl
    {
        [Key]
        [Column("indHdrCode")]
        public int IndHdrCode { get; set; }
        [Key]
        [Column("indHdrType")]
        public byte IndHdrType { get; set; }
        [Key]
        [Column("indItem")]
        [StringLength(15)]
        public string IndItem { get; set; }
        [Key]
        [Column("indItemDesc")]
        [StringLength(100)]
        public string IndItemDesc { get; set; }
        [Key]
        [Column("indUnitCost")]
        public float IndUnitCost { get; set; }
        [Column("indTotHours")]
        public float? IndTotHours { get; set; }
        [Column("indTotAmount")]
        public float? IndTotAmount { get; set; }
        [Column("indAddHours")]
        public float? IndAddHours { get; set; }
        [Column("indAddAmount")]
        public float? IndAddAmount { get; set; }
        [Column("indNet")]
        public float? IndNet { get; set; }
        [StringLength(10)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [StringLength(20)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        public byte? Deleted { get; set; }
        [StringLength(15)]
        public string DeletedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("job")]
        public int? Job { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Wdate { get; set; }
        [StringLength(50)]
        public string PassportNo { get; set; }
        [StringLength(50)]
        public string IkamaNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IkamaExpiryOn { get; set; }
        [StringLength(50)]
        public string EntryNo { get; set; }
        [StringLength(5)]
        public string MedicalClass { get; set; }
        public float? VisaFees { get; set; }
        public float? WorkLicense { get; set; }
        public float? Balance { get; set; }
        public float? Insurance { get; set; }
        public float? Saudization { get; set; }
        public float? Authorization { get; set; }
        public float? Ratification { get; set; }
        [Column("GOSI")]
        public float? Gosi { get; set; }
        [Column("indNhours")]
        public float? IndNhours { get; set; }
        [Column("indOThours")]
        public float? IndOthours { get; set; }
        [Column("indOTHrRate")]
        public double? IndOthrRate { get; set; }
        [Column("indTotWorkers")]
        public int? IndTotWorkers { get; set; }
        [Column("indTaxValue")]
        public double? IndTaxValue { get; set; }
        [Column("indNOTHrs")]
        public double? IndNothrs { get; set; }
        [Column("indNOTPay")]
        public double? IndNotpay { get; set; }
        [Column("indWEOTHrs")]
        public double? IndWeothrs { get; set; }
        [Column("indWEOTPay")]
        public double? IndWeotpay { get; set; }
        [Column("indHolOTHrs")]
        public double? IndHolOthrs { get; set; }
        [Column("indHolOTPay")]
        public double? IndHolOtpay { get; set; }
        [Column("indDayFee")]
        public double? IndDayFee { get; set; }
        [Column("indItemName")]
        [StringLength(150)]
        public string IndItemName { get; set; }
        [Column("invTotPresDays")]
        public int? InvTotPresDays { get; set; }
        [Column("invNHPay")]
        public double? InvNhpay { get; set; }
        [Column("indNHPay")]
        public double? IndNhpay { get; set; }
        [Column("indTotPresDays")]
        public double? IndTotPresDays { get; set; }

        [ForeignKey("IndHdrCode,IndHdrType")]
        [InverseProperty(nameof(TblInvHdr.TblInvDtls))]
        public virtual TblInvHdr IndHdr { get; set; }
    }
}
