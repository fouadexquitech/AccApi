using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblNetSalary")]
    public partial class TblNetSalary
    {
        [Required]
        [StringLength(14)]
        public string Seq { get; set; }
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labFileNo")]
        [StringLength(15)]
        public string LabFileNo { get; set; }
        [Column("labname")]
        [StringLength(75)]
        public string Labname { get; set; }
        [Column("labLAcc")]
        public double? LabLacc { get; set; }
        [Required]
        [Column("labProject")]
        [StringLength(50)]
        public string LabProject { get; set; }
        [Column("labPhoto")]
        [StringLength(75)]
        public string LabPhoto { get; set; }
        [Column("taxCashPAy")]
        public int? TaxCashPay { get; set; }
        [Column("codDescE")]
        [StringLength(100)]
        public string CodDescE { get; set; }
        [Column("labCoID")]
        public short? LabCoId { get; set; }
        [Column("taxrndbank")]
        public double? Taxrndbank { get; set; }
        [Column("labHrsDay")]
        public short? LabHrsDay { get; set; }
        [Column("codTax")]
        public byte? CodTax { get; set; }
        [Column("disProj")]
        public int? DisProj { get; set; }
        [Column("labFullPay")]
        public byte? LabFullPay { get; set; }
        [Column("labDownPay", TypeName = "money")]
        public decimal? LabDownPay { get; set; }
        [Column("labNew")]
        public byte? LabNew { get; set; }
        [Column("labSalType")]
        public byte? LabSalType { get; set; }
        [Column("labMonthFee", TypeName = "money")]
        public decimal? LabMonthFee { get; set; }
        [Column("labWEPayType")]
        public byte? LabWepayType { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [Column("labLDate", TypeName = "datetime")]
        public DateTime? LabLdate { get; set; }
        [Column("labWork")]
        public bool? LabWork { get; set; }
        [Column(TypeName = "money")]
        public decimal? OldDayFee { get; set; }
        [Column(TypeName = "money")]
        public decimal? DayFee { get; set; }
        [Column(TypeName = "money")]
        public decimal? OtherAllow { get; set; }
        public short? AllDays { get; set; }
        [Column("ND")]
        public short? Nd { get; set; }
        [Column("NDDays")]
        public short? Nddays { get; set; }
        public short? AbsentDays { get; set; }
        [Column("CS")]
        public short? Cs { get; set; }
        [Column("CSDays")]
        public short? Csdays { get; set; }
        [Column(TypeName = "money")]
        public decimal? VacHrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? VacPay { get; set; }
        [Column(TypeName = "money")]
        public decimal? HolHrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? HolPay { get; set; }
        [Column("HolOTHrs", TypeName = "money")]
        public decimal? HolOthrs { get; set; }
        [Column("HolOTPay", TypeName = "money")]
        public decimal? HolOtpay { get; set; }
        [Column(TypeName = "money")]
        public decimal? IdleHrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? IdleDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? IdlePay { get; set; }
        [Column(TypeName = "money")]
        public decimal? SumOfdisFood { get; set; }
        [Column("OTHrs", TypeName = "money")]
        public decimal? Othrs { get; set; }
        [Column("disAdditionalHrs", TypeName = "money")]
        public decimal? DisAdditionalHrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? PayNorm { get; set; }
        [Column(TypeName = "money")]
        public decimal? PayAcc { get; set; }
        [Column(TypeName = "money")]
        public decimal? PayOver { get; set; }
        [Column("WEOTPay", TypeName = "money")]
        public decimal? Weotpay { get; set; }
        [Column("WEOTHrs", TypeName = "money")]
        public decimal? Weothrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? SumOflonValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? SumOfSafety { get; set; }
        [Column("NDHrs", TypeName = "money")]
        public decimal? Ndhrs { get; set; }
        [Column("NDHrsAll", TypeName = "money")]
        public decimal? NdhrsAll { get; set; }
        [Column("WEPay", TypeName = "money")]
        public decimal? Wepay { get; set; }
        [Column("WEHrs", TypeName = "money")]
        public decimal? Wehrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? SumOfdisTotalPay { get; set; }
        [Column(TypeName = "money")]
        public decimal? LoanArc { get; set; }
        [Column(TypeName = "money")]
        public decimal? DailyPayTax { get; set; }
        [Column(TypeName = "money")]
        public decimal? NonTaxable { get; set; }
        [Column(TypeName = "money")]
        public decimal? Taxable { get; set; }
        [Column("SNREF")]
        public int? Snref { get; set; }
        [Column(TypeName = "money")]
        public decimal? Tax { get; set; }
        [Column(TypeName = "money")]
        public decimal? MonthlyTax { get; set; }
        [Column(TypeName = "money")]
        public decimal? DynamicTax { get; set; }
        [Column(TypeName = "money")]
        public decimal? LocalNet { get; set; }
        [Column(TypeName = "money")]
        public decimal? ProjectNet { get; set; }
        [Column(TypeName = "money")]
        public decimal? Net { get; set; }
        [StringLength(1)]
        public string TaxFixDyn { get; set; }
        [Column(TypeName = "money")]
        public decimal? F1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Reminder { get; set; }
        [Column("Final_Net1", TypeName = "money")]
        public decimal? FinalNet1 { get; set; }
        [Column(TypeName = "money")]
        public decimal? Round { get; set; }
        [Column(TypeName = "money")]
        public decimal? ExtraDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? PayrollDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? FixTax { get; set; }
        [Column(TypeName = "money")]
        public decimal? AbsentAmt { get; set; }
        [Column("PDays", TypeName = "money")]
        public decimal? Pdays { get; set; }
        [Column("WEDays", TypeName = "money")]
        public decimal? Wedays { get; set; }
        [Column(TypeName = "money")]
        public decimal? HolDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? VacDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalDays { get; set; }
        [Column(TypeName = "money")]
        public decimal? OtherAllowPerDay { get; set; }
        [Column(TypeName = "money")]
        public decimal? OtherAllowPerMonth { get; set; }
        [Column(TypeName = "money")]
        public decimal? WeekEndDays { get; set; }
        [Column("labFood", TypeName = "money")]
        public decimal? LabFood { get; set; }
        [Column("SS1", TypeName = "money")]
        public decimal? Ss1 { get; set; }
        [Column("SS2", TypeName = "money")]
        public decimal? Ss2 { get; set; }
        public int? LabSposor { get; set; }
        [StringLength(100)]
        public string LabSposorName { get; set; }
    }
}
