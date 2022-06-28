using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTaxRate")]
    public partial class TblTaxRate
    {
        [Key]
        [Column("taxProj")]
        public int TaxProj { get; set; }
        [Column("taxLastDat", TypeName = "datetime")]
        public DateTime? TaxLastDat { get; set; }
        [Column("taxRate")]
        public float? TaxRate { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("taxRndRate")]
        public short? TaxRndRate { get; set; }
        [Column("taxCurrency")]
        public double? TaxCurrency { get; set; }
        [Column("taxCSRate")]
        public double? TaxCsrate { get; set; }
        [Column("taxIdleRate")]
        public double? TaxIdleRate { get; set; }
        [Column("taxTimeIn", TypeName = "datetime")]
        public DateTime? TaxTimeIn { get; set; }
        [Column("taxHrsDay")]
        public short? TaxHrsDay { get; set; }
        [Column("taxPDays")]
        public int? TaxPdays { get; set; }
        [Column("taxNHRate")]
        public float? TaxNhrate { get; set; }
        [Column("taxWEHRate")]
        public float? TaxWehrate { get; set; }
        [Column("taxLunchTime")]
        public float? TaxLunchTime { get; set; }
        [Column("taxDaysWeek")]
        public short? TaxDaysWeek { get; set; }
        [Column("taxWEManual")]
        public short? TaxWemanual { get; set; }
        [Column("taxWEManualDays")]
        public short? TaxWemanualDays { get; set; }
        [Column("taxFood")]
        public float? TaxFood { get; set; }
        [Column("taxLogo1")]
        [StringLength(75)]
        public string TaxLogo1 { get; set; }
        [Column("taxLogo2")]
        [StringLength(75)]
        public string TaxLogo2 { get; set; }
        [Column("taxFixedTax")]
        public short? TaxFixedTax { get; set; }
        [Column("taxFoodFrac")]
        public short? TaxFoodFrac { get; set; }
        [Column("taxTVA")]
        public short? TaxTva { get; set; }
        [Column("taxIdleWE")]
        public short? TaxIdleWe { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("taxPreliems")]
        public bool? TaxPreliems { get; set; }
        [Column("taxBudgetCost")]
        public float? TaxBudgetCost { get; set; }
        [Column("taxLogoSti")]
        public bool? TaxLogoSti { get; set; }
        [Column("taxTimeBarCode")]
        public bool? TaxTimeBarCode { get; set; }
        [Column("taxExpTax")]
        public float? TaxExpTax { get; set; }
        [Column("taxEmpTax")]
        public float? TaxEmpTax { get; set; }
        [Column("taxFixedTaxEmp")]
        public short? TaxFixedTaxEmp { get; set; }
        [Column("taxAbsSPHR")]
        public bool? TaxAbsSphr { get; set; }
        [Column("taxSkipConf")]
        public bool? TaxSkipConf { get; set; }
        [Column("taxShowStopLab")]
        public bool? TaxShowStopLab { get; set; }
        [Column("taxPhotoPath")]
        [StringLength(40)]
        public string TaxPhotoPath { get; set; }
        [Column("taxOTFixTax")]
        public float? TaxOtfixTax { get; set; }
        [Column("taxFAcc")]
        public double? TaxFacc { get; set; }
        [Column("taxbank")]
        [StringLength(50)]
        public string Taxbank { get; set; }
        [Column("taxrndbank")]
        public double? Taxrndbank { get; set; }
        [Column("taxCashPAy")]
        public int? TaxCashPay { get; set; }
        [Column("labBB")]
        public int? LabBb { get; set; }
        [Column("taxDailyMonthly")]
        public short? TaxDailyMonthly { get; set; }
        [Column("taxLockToDate", TypeName = "datetime")]
        public DateTime? TaxLockToDate { get; set; }
        [Column("taxBarCodeFile")]
        [StringLength(255)]
        public string TaxBarCodeFile { get; set; }
        [Column("taxServerPath")]
        [StringLength(255)]
        public string TaxServerPath { get; set; }
        [Column("taxOverHead1", TypeName = "smallmoney")]
        public decimal? TaxOverHead1 { get; set; }
        [Column("taxOverHead2", TypeName = "smallmoney")]
        public decimal? TaxOverHead2 { get; set; }
        [Column("taxOverHead3", TypeName = "smallmoney")]
        public decimal? TaxOverHead3 { get; set; }
        [Column("taxSickLeaveDays")]
        public byte? TaxSickLeaveDays { get; set; }
        [Column("taxSS1")]
        public double? TaxSs1 { get; set; }
        [Column("taxSS2")]
        public double? TaxSs2 { get; set; }
        [Column("taxHolHRate")]
        public float? TaxHolHrate { get; set; }
        [Column("taxRndCeil")]
        public byte? TaxRndCeil { get; set; }
        [Column("taxContMedicalUnion")]
        public double? TaxContMedicalUnion { get; set; }
        [Column("taxContChochesion")]
        public double? TaxContChochesion { get; set; }
        [Column("taxContReduduncy")]
        public double? TaxContReduduncy { get; set; }
        [Column("taxContIndustrial")]
        public double? TaxContIndustrial { get; set; }
        [Column("taxContUnion")]
        public double? TaxContUnion { get; set; }
        [Column("taxDedProvidentFund")]
        public double? TaxDedProvidentFund { get; set; }
        [Column("taxDedSocialIns")]
        public double? TaxDedSocialIns { get; set; }
        [Column("taxDedMedicalFixed")]
        public double? TaxDedMedicalFixed { get; set; }
        [Column("taxDedUnionsFund")]
        public double? TaxDedUnionsFund { get; set; }
        [Column("taxContProvidentFund")]
        public double? TaxContProvidentFund { get; set; }
        [Column("taxContUnions")]
        public double? TaxContUnions { get; set; }
        [Column("taxContLeave")]
        public double? TaxContLeave { get; set; }
        [Column("taxContSocialIns")]
        public double? TaxContSocialIns { get; set; }
        [Column("taxContMedicalFixed")]
        public double? TaxContMedicalFixed { get; set; }
        [Column("taxContUnionsFund")]
        public double? TaxContUnionsFund { get; set; }
        [Column("taxFromDate", TypeName = "date")]
        public DateTime? TaxFromDate { get; set; }
    }
}
