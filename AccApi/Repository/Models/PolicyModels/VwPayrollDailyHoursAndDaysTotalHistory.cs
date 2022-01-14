using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class VwPayrollDailyHoursAndDaysTotalHistory
    {
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column("disLab")]
        [StringLength(15)]
        public string DisLab { get; set; }
        [Column("disWBS")]
        [StringLength(15)]
        public string DisWbs { get; set; }
        [Column("disStatus")]
        public short? DisStatus { get; set; }
        [Column("disHours")]
        public double? DisHours { get; set; }
        [Column("disContraHrs")]
        public double? DisContraHrs { get; set; }
        [Column("disProject")]
        [StringLength(9)]
        public string DisProject { get; set; }
        [Column("disProjectDef")]
        [StringLength(9)]
        public string DisProjectDef { get; set; }
        [Column("disSummerHrs")]
        public double? DisSummerHrs { get; set; }
        [Column("disDailyHours")]
        public float? DisDailyHours { get; set; }
        public double? ContraHours { get; set; }
        [Column("PDaysNH")]
        public double? PdaysNh { get; set; }
        [Column("PDays")]
        public int Pdays { get; set; }
        [Column("PWEDays")]
        public int Pwedays { get; set; }
        [Column("PHolDays")]
        public int PholDays { get; set; }
        [Column(TypeName = "numeric(12, 1)")]
        public decimal? SickLeaveDays { get; set; }
        public int AccidentDays { get; set; }
        public int AbsentDays { get; set; }
        [Column("OTHrs")]
        public double? Othrs { get; set; }
        public double? ContraN { get; set; }
        [Column("xOTHrsWE")]
        public double? XOthrsWe { get; set; }
        [Column("ContraWE")]
        public double? ContraWe { get; set; }
        [Column("OTHrsHol")]
        public double OthrsHol { get; set; }
        public double? ContraHol { get; set; }
        public double? WorkDays { get; set; }
        [Column(TypeName = "numeric(9, 6)")]
        public decimal? FracDays { get; set; }
        public double? TotalDays { get; set; }
        [Column("TotalDaysNH")]
        public double? TotalDaysNh { get; set; }
        public float? DailyHours { get; set; }
        [Column("disNH")]
        public float? DisNh { get; set; }
        [Column("WEDays", TypeName = "numeric(17, 6)")]
        public decimal? Wedays { get; set; }
        public int? HolDays { get; set; }
        public int FracHours { get; set; }
        [Column(TypeName = "numeric(22, 6)")]
        public decimal? AllDays { get; set; }
        public bool? Confirmed { get; set; }
        public bool? Exported { get; set; }
        [Column(TypeName = "numeric(9, 6)")]
        public decimal? AbsentDaysFrac { get; set; }
        public int AbsentHolDays { get; set; }
        [Column("disNight")]
        [StringLength(5)]
        public string DisNight { get; set; }
        [Column("disforman")]
        public int? Disforman { get; set; }
        [Column("disDeleted")]
        public byte? DisDeleted { get; set; }
        [Column("disArea")]
        public int? DisArea { get; set; }
        [Column("disDesig")]
        public int? DisDesig { get; set; }
        [Column("OTHRSWE")]
        public double Othrswe { get; set; }
        [Column("NH")]
        public float? Nh { get; set; }
        [Column("disLocation")]
        public int? DisLocation { get; set; }
    }
}
