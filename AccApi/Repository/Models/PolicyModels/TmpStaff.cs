using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tmpStaff")]
    public partial class TmpStaff
    {
        [Key]
        [Column("sSeq")]
        public long SSeq { get; set; }
        [Key]
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(75)]
        public string JobDesc { get; set; }
        public int? JobTitle { get; set; }
        [StringLength(150)]
        public string Grp { get; set; }
        public short? Sort { get; set; }
        public short? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeIn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TimeOut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BreakOut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BreakIn { get; set; }
        [StringLength(6)]
        public string EmpId { get; set; }
        [Column("PSC")]
        [StringLength(10)]
        public string Psc { get; set; }
        [StringLength(100)]
        public string EmpName { get; set; }
        [StringLength(9)]
        public string ProjectDef { get; set; }
        [Column("DN")]
        [StringLength(5)]
        public string Dn { get; set; }
        [Column("SponsorID")]
        public int? SponsorId { get; set; }
        [StringLength(50)]
        public string Sponsor { get; set; }
        [StringLength(30)]
        public string StatusDesc { get; set; }
        [Column("ProjID")]
        public short? ProjId { get; set; }
        public bool? ShowOnAbsent { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastAbsentDate { get; set; }
        public short? LastStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastPresentDate { get; set; }
        [StringLength(30)]
        public string AbsentFrom { get; set; }
        public byte? Daily { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JoinDate { get; set; }
        public int? OtherSorts { get; set; }
        public byte? ShowDailyRpt { get; set; }
        [Column("grpSort")]
        public int? GrpSort { get; set; }
        [StringLength(100)]
        public string SubGrp { get; set; }
        [Column("subGrpSort")]
        public int? SubGrpSort { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(100)]
        public string WorkTypeDesc { get; set; }
        public int? Worktype { get; set; }
        public int? PlanHistogram { get; set; }
    }
}
