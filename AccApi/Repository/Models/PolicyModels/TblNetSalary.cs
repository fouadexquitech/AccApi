using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblNetSalary")]
    public partial class TblNetSalary
    {
        [Key]
        [StringLength(14)]
        public string Seq { get; set; }
        [StringLength(50)]
        public string ProjectName { get; set; }
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
        public int? LastProject { get; set; }
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
        [StringLength(20)]
        public string LastProjectDef { get; set; }
        [Column("labFullPay")]
        public byte? LabFullPay { get; set; }
        [Column("labDownPay")]
        public double? LabDownPay { get; set; }
        [Column("labNew")]
        public byte? LabNew { get; set; }
        [Column("labSalType")]
        public byte? LabSalType { get; set; }
        [Column("labMonthFee")]
        public double? LabMonthFee { get; set; }
        [Column("labWEPayType")]
        public byte? LabWepayType { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [Column("labWDate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [Column("labLDate", TypeName = "datetime")]
        public DateTime? LabLdate { get; set; }
        [Column("labWork")]
        public bool? LabWork { get; set; }
        public double? OldDayFee { get; set; }
        public double? DayFee { get; set; }
        public double? OtherAllow { get; set; }
        public double? TransportAllow { get; set; }
        public double? HousingAllow { get; set; }
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
        public double? VacHrs { get; set; }
        public double? VacPay { get; set; }
        public double? HolHrs { get; set; }
        public double? HolPay { get; set; }
        [Column("HolOTHrs")]
        public double? HolOthrs { get; set; }
        [Column("HolOTPay")]
        public double? HolOtpay { get; set; }
        public double? IdleHrs { get; set; }
        public double? IdleDays { get; set; }
        public double? IdlePay { get; set; }
        public double? SumOfdisFood { get; set; }
        [Column("OTHrs")]
        public double? Othrs { get; set; }
        public double? ContraHrs { get; set; }
        public double? PayNorm { get; set; }
        public double? PayAcc { get; set; }
        public double? PayOver { get; set; }
        public double? PayContra { get; set; }
        [Column("WEOTPay")]
        public double? Weotpay { get; set; }
        [Column("WEOTHrs")]
        public double? Weothrs { get; set; }
        public double? SumOflonValue { get; set; }
        public double? SumOfSafety { get; set; }
        [Column("NDHrs")]
        public double? Ndhrs { get; set; }
        [Column("NDHrsAll")]
        public double? NdhrsAll { get; set; }
        [Column("WEPay")]
        public double? Wepay { get; set; }
        [Column("WEHrs")]
        public double? Wehrs { get; set; }
        public double? SumOfdisTotalPay { get; set; }
        public double? LoanArc { get; set; }
        public double? DailyPayTax { get; set; }
        public double? NonTaxable { get; set; }
        public double? Taxable { get; set; }
        [Column("SNREF")]
        public int Snref { get; set; }
        public double? Tax { get; set; }
        public double? MonthlyTax { get; set; }
        public double? DynamicTax { get; set; }
        public double? LocalNet { get; set; }
        public double? ProjectNet { get; set; }
        public double? Net { get; set; }
        [StringLength(1)]
        public string TaxFixDyn { get; set; }
        public double? F1 { get; set; }
        public double? Reminder { get; set; }
        [Column("Final_Net1")]
        public double? FinalNet1 { get; set; }
        public double? Round { get; set; }
        public double? ExtraDays { get; set; }
        public double? PayrollDays { get; set; }
        public double? FixTax { get; set; }
        public double? AbsentAmt { get; set; }
        [Column("PDays")]
        public double? Pdays { get; set; }
        [Column("WEDays")]
        public double? Wedays { get; set; }
        public double? HolDays { get; set; }
        public double? VacDays { get; set; }
        public double? TotalDays { get; set; }
        public double? OtherAllowPerDay { get; set; }
        public double? OtherAllowPerMonth { get; set; }
        public double? WeekEndDays { get; set; }
        [Column("labFood")]
        public double? LabFood { get; set; }
        [Column("SS1")]
        public double? Ss1 { get; set; }
        [Column("SS2")]
        public double? Ss2 { get; set; }
        public bool? LabWithoutTax { get; set; }
        [Column("labIdNo")]
        [StringLength(50)]
        public string LabIdNo { get; set; }
        public double? ExchRate { get; set; }
        public int? SkillGroup { get; set; }
        [StringLength(100)]
        public string SkillGroupDesc { get; set; }
        [Column("labSkill")]
        public int? LabSkill { get; set; }
        [Column("labJob")]
        public int? LabJob { get; set; }
        [Column("OTwithoutContHrs")]
        public double? OtwithoutContHrs { get; set; }
        public double? ContraHrsHol { get; set; }
        [Column("ContraHrsWE")]
        public double? ContraHrsWe { get; set; }
        public double? TotalHrs { get; set; }
        public double? PaySmr { get; set; }
        public double? SmrHrs { get; set; }
        public double? SmrHrsHol { get; set; }
        [Column("SmrHrsWE")]
        public double? SmrHrsWe { get; set; }
        [StringLength(25)]
        public string UserName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
    }
}
