using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTempReports")]
    public partial class TblTempReport
    {
        [Key]
        [Column("rSeq")]
        public long RSeq { get; set; }
        [Column("disProj")]
        [StringLength(20)]
        public string DisProj { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [StringLength(50)]
        public string Job { get; set; }
        [Column("labName")]
        [StringLength(75)]
        public string LabName { get; set; }
        [Column("labNameE")]
        [StringLength(75)]
        public string LabNameE { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [StringLength(30)]
        public string Nationality { get; set; }
        [Column("disArea")]
        public int? DisArea { get; set; }
        [StringLength(30)]
        public string Area { get; set; }
        [Column("disProjectDef")]
        [StringLength(20)]
        public string DisProjectDef { get; set; }
        [Column("labId")]
        [StringLength(8)]
        public string LabId { get; set; }
        [Column("disForman")]
        public int? DisForman { get; set; }
        [StringLength(50)]
        public string Forman { get; set; }
        public double? Hrs { get; set; }
        [Column("TotalPayUSD")]
        public float? TotalPayUsd { get; set; }
        [Column("dailyPay")]
        public float? DailyPay { get; set; }
        [StringLength(5)]
        public string ClassSalary { get; set; }
        [Column("disNight")]
        [StringLength(5)]
        public string DisNight { get; set; }
        [Column("disStatus")]
        public short? DisStatus { get; set; }
        [Column("disLab")]
        [StringLength(10)]
        public string DisLab { get; set; }
        [Column("labJob")]
        public int? LabJob { get; set; }
        [Column("labDsg")]
        public int? LabDsg { get; set; }
        [StringLength(200)]
        public string DsgDesc { get; set; }
        [Column("disProject")]
        [StringLength(12)]
        public string DisProject { get; set; }
        [StringLength(50)]
        public string Sponsor { get; set; }
        [StringLength(150)]
        public string Grp { get; set; }
        [Column("mpMainSponsor")]
        public int? MpMainSponsor { get; set; }
        [Column("mpType")]
        public byte? MpType { get; set; }
        [Column("mpID")]
        public int? MpId { get; set; }
        public short? Sort { get; set; }
        [Column("cumulative")]
        public int? Cumulative { get; set; }
        [Column("labWdate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastAbsentDate { get; set; }
        public short? LastStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastPresentDate { get; set; }
        [StringLength(30)]
        public string AbsentFrom { get; set; }
        public short? WorkType { get; set; }
        public byte? ReportType { get; set; }
        [Column("ProjID")]
        public short? ProjId { get; set; }
        public int? ShowOnAbsent { get; set; }
        public byte? Monthly { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string MainSponsor { get; set; }
        [StringLength(30)]
        public string StatusDesc { get; set; }
        public int? OtherSorts { get; set; }
        [StringLength(100)]
        public string HistogramJobDesc { get; set; }
        public int? HistogramCount { get; set; }
        public int? LaborsCount { get; set; }
        [Column("grpSort")]
        public int? GrpSort { get; set; }
        [StringLength(100)]
        public string SubGrp { get; set; }
        [Column("subGrpSort")]
        public int? SubGrpSort { get; set; }
        [Column("disOccupGrp")]
        public int? DisOccupGrp { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(100)]
        public string WorkTypeDesc { get; set; }
        [StringLength(100)]
        public string ProjectName { get; set; }
        [StringLength(100)]
        public string ProjectCountry { get; set; }
    }
}
