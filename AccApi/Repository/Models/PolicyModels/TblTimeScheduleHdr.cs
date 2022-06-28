using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTimeScheduleHdr")]
    public partial class TblTimeScheduleHdr
    {
        [Key]
        [Column("tshProjID")]
        public int TshProjId { get; set; }
        [Key]
        [Column("tshProjectDef")]
        [StringLength(9)]
        public string TshProjectDef { get; set; }
        [Column("tshCSRate")]
        public short? TshCsrate { get; set; }
        [Column("tshIdleRate")]
        public short? TshIdleRate { get; set; }
        [Column("tshPDays")]
        public int? TshPdays { get; set; }
        [Column("tshYearVacRange")]
        public short? TshYearVacRange { get; set; }
        [Column("tshMonWorkHr")]
        public short? TshMonWorkHr { get; set; }
        [Column("tshDelayRateMins")]
        public float? TshDelayRateMins { get; set; }
        [Column("tshDelayRateHrs")]
        public float? TshDelayRateHrs { get; set; }
        [Column("tshNHRate")]
        public float? TshNhrate { get; set; }
        [Column("tshWEHRate")]
        public float? TshWehrate { get; set; }
        [Column("tshSalMethod")]
        public short? TshSalMethod { get; set; }
        [Column("tshYearVac")]
        public byte? TshYearVac { get; set; }
        [Column("tshVacPay")]
        public bool? TshVacPay { get; set; }
        [Column("tshLunchRate")]
        public float? TshLunchRate { get; set; }
    }
}
