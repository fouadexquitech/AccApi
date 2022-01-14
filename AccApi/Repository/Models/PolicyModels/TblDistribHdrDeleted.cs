using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDistribHdrDeleted")]
    public partial class TblDistribHdrDeleted
    {
        [Key]
        public long Seq { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column("disLab")]
        [StringLength(10)]
        public string DisLab { get; set; }
        [Column("disTimein", TypeName = "datetime")]
        public DateTime? DisTimein { get; set; }
        [Column("disTimeout", TypeName = "datetime")]
        public DateTime? DisTimeout { get; set; }
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
        [StringLength(20)]
        public string DisProject { get; set; }
        [Column("disEntry")]
        public byte? DisEntry { get; set; }
        [Column("disProjectDef")]
        [StringLength(20)]
        public string DisProjectDef { get; set; }
        [Column("disTimeInAct", TypeName = "datetime")]
        public DateTime? DisTimeInAct { get; set; }
        [Column("disTimeOutAct", TypeName = "datetime")]
        public DateTime? DisTimeOutAct { get; set; }
        [Column("disNight")]
        [StringLength(5)]
        public string DisNight { get; set; }
        [Column("disforman")]
        public int? Disforman { get; set; }
        [Column("disTimeinRnd", TypeName = "datetime")]
        public DateTime? DisTimeinRnd { get; set; }
        [Column("disTimeoutRnd", TypeName = "datetime")]
        public DateTime? DisTimeoutRnd { get; set; }
        public bool? Confirmed { get; set; }
        [StringLength(10)]
        public string ConfirmedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmedDate { get; set; }
        public bool? Exported { get; set; }
        [StringLength(10)]
        public string ExportedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExportedDate { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(10)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column("disHrsDay")]
        public float? DisHrsDay { get; set; }
        [Column("disLunchBreakHrs")]
        public float? DisLunchBreakHrs { get; set; }
        [Column("disIdleHrs")]
        public float? DisIdleHrs { get; set; }
        [Column("disWE")]
        public byte? DisWe { get; set; }
        [Column("disHol")]
        public byte? DisHol { get; set; }
        [Column("disDeleted")]
        public byte? DisDeleted { get; set; }
        [Column("disDeletedBy")]
        [StringLength(50)]
        public string DisDeletedBy { get; set; }
        [Column("disDeletedOn", TypeName = "datetime")]
        public DateTime? DisDeletedOn { get; set; }
        [Column("disSickRate")]
        public double? DisSickRate { get; set; }
        [Column("disSummerHrs")]
        public double? DisSummerHrs { get; set; }
        [Column("disNH")]
        public float? DisNh { get; set; }
        [Column("disEarlyHrs")]
        public float? DisEarlyHrs { get; set; }
        [Column("disDailyHours")]
        public float? DisDailyHours { get; set; }
        [Column("disLunchBreak")]
        public bool? DisLunchBreak { get; set; }
        [Column("disDeductionHrs")]
        public double? DisDeductionHrs { get; set; }
        [Column("disNonProdHrs")]
        public float? DisNonProdHrs { get; set; }
        [Column("disNonProdPay")]
        public float? DisNonProdPay { get; set; }
        [Column("disPayNH")]
        public double? DisPayNh { get; set; }
        [Column("disArea")]
        public int? DisArea { get; set; }
        [Column("disTotalOTHrs")]
        public double? DisTotalOthrs { get; set; }
        [Column("disTotalPayOver")]
        public double? DisTotalPayOver { get; set; }
        [Column("disProdPay")]
        public float? DisProdPay { get; set; }
        [Column("disProdHrs")]
        public double? DisProdHrs { get; set; }
        [Column("disOTHrs")]
        public float? DisOthrs { get; set; }
        [Column("disWEOTHrs")]
        public double? DisWeothrs { get; set; }
        [Column("disHolOTHrs")]
        public double? DisHolOthrs { get; set; }
        [Column("disWEOTPay")]
        public float? DisWeotpay { get; set; }
        [Column("disVacHrs")]
        public float? DisVacHrs { get; set; }
        [Column("disVacPay")]
        public float? DisVacPay { get; set; }
        [Column("disTotalPay")]
        public float? DisTotalPay { get; set; }
        [Column("disFood")]
        public double? DisFood { get; set; }
        [Column("disDayFeeCS")]
        public double? DisDayFeeCs { get; set; }
        [Column("disHrsDaySchedule")]
        public float? DisHrsDaySchedule { get; set; }
        [Column("disDayFee")]
        public float? DisDayFee { get; set; }
        [Column("disNorHrsday")]
        public float? DisNorHrsday { get; set; }
        [Column("disWEHrs")]
        public float? DisWehrs { get; set; }
        [Column("disHolHrs")]
        public double? DisHolHrs { get; set; }
        [Column("disWEPay")]
        public double? DisWepay { get; set; }
        [Column("disHolPay")]
        public double? DisHolPay { get; set; }
        [Column("disPayOver")]
        public double? DisPayOver { get; set; }
        [Column("disHolOTPay")]
        public double? DisHolOtpay { get; set; }
        [Column("disfirstAtt")]
        public byte? DisfirstAtt { get; set; }
        [Column("disPrevHrs")]
        public double? DisPrevHrs { get; set; }
        [Column("disDailyPayTax")]
        public double? DisDailyPayTax { get; set; }
        [Column("disExchRate")]
        public double? DisExchRate { get; set; }
        [Column("disPayAcc")]
        public double? DisPayAcc { get; set; }
        [Column("disNonTaxable")]
        public double? DisNonTaxable { get; set; }
        [Column("disWeek")]
        public double? DisWeek { get; set; }
        [Column("disDesig")]
        public int? DisDesig { get; set; }
        [Column("disLocation")]
        public int? DisLocation { get; set; }
        [Column("disLunchBreakWrkHrs")]
        public float? DisLunchBreakWrkHrs { get; set; }
        [StringLength(15)]
        public string UserOpenExport { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOpenExport { get; set; }
        [Column("disRecalc")]
        public byte? DisRecalc { get; set; }
        [Column("disRecalcBy")]
        [StringLength(20)]
        public string DisRecalcBy { get; set; }
        [Column("disRecalcOn", TypeName = "datetime")]
        public DateTime? DisRecalcOn { get; set; }
        [Column("disSponsor")]
        public int? DisSponsor { get; set; }
        [Column("disBldg")]
        public int? DisBldg { get; set; }
        [Column("disTransportPay")]
        public double? DisTransportPay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DteInsFromHdr { get; set; }
        [Column("disJob")]
        public int? DisJob { get; set; }
        [Column("disHousingPay")]
        public double? DisHousingPay { get; set; }
        [Column("disOccupGrp")]
        public int? DisOccupGrp { get; set; }
    }
}
