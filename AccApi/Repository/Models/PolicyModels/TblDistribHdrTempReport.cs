using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDistribHdr_Temp_Report")]
    [Index(nameof(DisDate), Name = "IX_tblDistribHdr_Temp_Report")]
    [Index(nameof(Dislab), Name = "IX_tblDistribHdr_Temp_Report_1")]
    [Index(nameof(DisProject), Name = "IX_tblDistribHdr_Temp_Report_2")]
    public partial class TblDistribHdrTempReport
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column("dislab")]
        [StringLength(8)]
        public string Dislab { get; set; }
        [Column("disProject")]
        [StringLength(15)]
        public string DisProject { get; set; }
        [Column("disProjectDef")]
        [StringLength(30)]
        public string DisProjectDef { get; set; }
        [Column("disEntry")]
        public byte? DisEntry { get; set; }
        [Column("disStatus")]
        public short? DisStatus { get; set; }
        [Column("disNight")]
        [StringLength(5)]
        public string DisNight { get; set; }
        [Column("disHours")]
        public double? DisHours { get; set; }
        [Column("disNorHrsday")]
        public double? DisNorHrsday { get; set; }
        [Column("disContraHrs")]
        public double? DisContraHrs { get; set; }
        [Column("disSummerHrs")]
        public double? DisSummerHrs { get; set; }
        [Column("disOTHrs")]
        public double? DisOthrs { get; set; }
        [Column("disWEOTHrs")]
        public double? DisWeothrs { get; set; }
        [Column("disDeductionHrs")]
        public double? DisDeductionHrs { get; set; }
        [Column("disLunchBreakWrkHrs")]
        public double? DisLunchBreakWrkHrs { get; set; }
        [Column("disWEHrs")]
        public double? DisWehrs { get; set; }
        [Column("disHolHrs")]
        public double? DisHolHrs { get; set; }
        [Column("disVacHrs")]
        public double? DisVacHrs { get; set; }
        [Column("disLunchBreak")]
        public bool? DisLunchBreak { get; set; }
        public bool? Exported { get; set; }
        public bool? Confirmed { get; set; }
        [Column("disHolOTHrs")]
        public double? DisHolOthrs { get; set; }
        [Column("disOTHrsN")]
        public double? DisOthrsN { get; set; }
        [Column("disWEOTHrsN")]
        public double? DisWeothrsN { get; set; }
        [Column("disHolOTHrsN")]
        public double? DisHolOthrsN { get; set; }
        [Column("disContraHrsN")]
        public double? DisContraHrsN { get; set; }
        public byte? HideWeekEnd { get; set; }
        [Column("WERequired")]
        public int? Werequired { get; set; }
        [Column("WEPresent")]
        public int? Wepresent { get; set; }
        [Column("WEAbsent")]
        public int? Weabsent { get; set; }
        [Column("disWE")]
        public byte? DisWe { get; set; }
        [Column("disHol")]
        public byte? DisHol { get; set; }
        [Column("disWBS")]
        [StringLength(50)]
        public string DisWbs { get; set; }
        [Column("disArea")]
        public int? DisArea { get; set; }
        [Column("disForman")]
        public int? DisForman { get; set; }
        [StringLength(100)]
        public string Sponsor { get; set; }
        [Column("jcDesc")]
        [StringLength(150)]
        public string JcDesc { get; set; }
        [StringLength(200)]
        public string LabName { get; set; }
        [Column("disRecalc")]
        public byte? DisRecalc { get; set; }
        [StringLength(20)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column("disTimeinRnd", TypeName = "datetime")]
        public DateTime? DisTimeinRnd { get; set; }
        [Column("disTimeoutRnd", TypeName = "datetime")]
        public DateTime? DisTimeoutRnd { get; set; }
        [Column("disTimeout", TypeName = "datetime")]
        public DateTime? DisTimeout { get; set; }
        [Column("disTimeIn", TypeName = "datetime")]
        public DateTime? DisTimeIn { get; set; }
        [StringLength(20)]
        public string ConfirmedBy { get; set; }
        [StringLength(15)]
        public string UserOpenExport { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOpenExport { get; set; }
        [StringLength(15)]
        public string ExportedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExportedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmedDate { get; set; }
        [Column("TotPresWEHours")]
        public double? TotPresWehours { get; set; }
        [Column("disInsertedDate", TypeName = "date")]
        public DateTime? DisInsertedDate { get; set; }
        [Column("disLastPresentDate", TypeName = "datetime")]
        public DateTime? DisLastPresentDate { get; set; }
        [Column("disFirstPresentDate", TypeName = "datetime")]
        public DateTime? DisFirstPresentDate { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [Column("TotWEContHours")]
        public double? TotWecontHours { get; set; }
        [Column("disTimeinAct", TypeName = "datetime")]
        public DateTime? DisTimeinAct { get; set; }
        [Column("disTimeoutAct", TypeName = "datetime")]
        public DateTime? DisTimeoutAct { get; set; }
        [Column("disLunchBreakHrs")]
        public float? DisLunchBreakHrs { get; set; }
    }
}
