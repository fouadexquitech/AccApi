using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class VwPayrollDailyAmountsHistory
    {
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column("disProject")]
        [StringLength(9)]
        public string DisProject { get; set; }
        [Column("disProjectDef")]
        [StringLength(9)]
        public string DisProjectDef { get; set; }
        [Column("disWBS")]
        [StringLength(15)]
        public string DisWbs { get; set; }
        [Column("disLab")]
        [StringLength(15)]
        public string DisLab { get; set; }
        [Required]
        [Column("labId")]
        [StringLength(15)]
        public string LabId { get; set; }
        [Column("labName")]
        [StringLength(200)]
        public string LabName { get; set; }
        [Column("labjob")]
        public int? Labjob { get; set; }
        [Column("labBankAcc")]
        [StringLength(255)]
        public string LabBankAcc { get; set; }
        [Column("labWDate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [Column("labLDate", TypeName = "datetime")]
        public DateTime? LabLdate { get; set; }
        [Column("labDayFee")]
        public double LabDayFee { get; set; }
        [Column("labTransport")]
        public double LabTransport { get; set; }
        [Column("labFood")]
        public double LabFood { get; set; }
        [Column("labFixedMonthly")]
        public double LabFixedMonthly { get; set; }
        [Column("labHousing")]
        public double LabHousing { get; set; }
        [Column("labPhoneAllow")]
        public double LabPhoneAllow { get; set; }
        [Column("labPositionNo")]
        [StringLength(10)]
        public string LabPositionNo { get; set; }
        [Column("disHours")]
        public double? DisHours { get; set; }
        public double? ContraHours { get; set; }
        [Column("PDays")]
        public int Pdays { get; set; }
        [Column("WEDays", TypeName = "numeric(17, 6)")]
        public decimal? Wedays { get; set; }
        public int? HolDays { get; set; }
        [Column(TypeName = "numeric(12, 1)")]
        public decimal? SickLeaveDays { get; set; }
        public int AccidentDays { get; set; }
        public int AbsentDays { get; set; }
        [Column("disNH")]
        public float? DisNh { get; set; }
        [Column("OTHrs")]
        public double? Othrs { get; set; }
        [Column("OTHRSWE")]
        public double Othrswe { get; set; }
        [Column("OTHrsHol")]
        public double OthrsHol { get; set; }
        public double? ContraN { get; set; }
        [Column("ContraWE")]
        public double? ContraWe { get; set; }
        public double? ContraHol { get; set; }
        public float? DailyHours { get; set; }
        [Column("NH_Pay")]
        public double? NhPay { get; set; }
        [Column("SickLeave_Pay")]
        public double? SickLeavePay { get; set; }
        [Column("OT_Pay")]
        public double? OtPay { get; set; }
        [Column("OTWE_Pay")]
        public double? OtwePay { get; set; }
        [Column("OTHol_Pay")]
        public double? OtholPay { get; set; }
        [Column("Frac_Pay")]
        public double? FracPay { get; set; }
        [Column("Hol_Pay")]
        public double? HolPay { get; set; }
        [Column("ContraN_Pay")]
        public double? ContraNPay { get; set; }
        [Column("ContraWE_Pay")]
        public double? ContraWePay { get; set; }
        [Column("ContraHol_Pay")]
        public double? ContraHolPay { get; set; }
        public double? Transport { get; set; }
        public double? Food { get; set; }
        [Column("PWEDays")]
        public int Pwedays { get; set; }
        [Column("PHolDays")]
        public int PholDays { get; set; }
        public int FracHours { get; set; }
        [Column(TypeName = "numeric(9, 6)")]
        public decimal? FracDays { get; set; }
        public double? WorkDays { get; set; }
        public double? TotalDays { get; set; }
        [Column("TotalDaysNH")]
        public double? TotalDaysNh { get; set; }
        [Column(TypeName = "numeric(22, 6)")]
        public decimal? AllDays { get; set; }
        public int OtherAdditions { get; set; }
        public int OtherDeductions { get; set; }
        public bool? Confirmed { get; set; }
        public bool? Exported { get; set; }
        [Column("labHidden")]
        public byte? LabHidden { get; set; }
        [Column(TypeName = "numeric(9, 6)")]
        public decimal? AbsentDaysFrac { get; set; }
        [Column("WE_Pay")]
        public int WePay { get; set; }
        [Column("disNight")]
        [StringLength(5)]
        public string DisNight { get; set; }
        [Column("disforman")]
        public int? Disforman { get; set; }
        [Column("disProdHrs")]
        public double? DisProdHrs { get; set; }
        [Column("disNonProdPay")]
        public double? DisNonProdPay { get; set; }
        [Column("disNonProdHrs")]
        public float DisNonProdHrs { get; set; }
        [Column("disStatus")]
        public short? DisStatus { get; set; }
        [Column("disDeleted")]
        public byte? DisDeleted { get; set; }
        [Column("disArea")]
        public int? DisArea { get; set; }
        public double? FixedMonthly { get; set; }
        [Column("WE_FoodPay")]
        public double? WeFoodPay { get; set; }
        [Column("OT_WE_HrRate")]
        public float OtWeHrRate { get; set; }
        [Column("OT_HrRate")]
        public float OtHrRate { get; set; }
        [Column("labSkilled")]
        public byte? LabSkilled { get; set; }
        [Column("labSponsor")]
        public int? LabSponsor { get; set; }
        [Column("disDesig")]
        public int? DisDesig { get; set; }
        [Column("arZone")]
        [StringLength(12)]
        public string ArZone { get; set; }
        [Column("NH")]
        public float? Nh { get; set; }
        [Column("disLocation")]
        public int? DisLocation { get; set; }
        public float DailyAuxHrs { get; set; }
    }
}
