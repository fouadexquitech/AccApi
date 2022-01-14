using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("Copy Of tblTimeSchedule")]
    public partial class CopyOfTblTimeSchedule
    {
        [Column("tsdProjID")]
        public int TsdProjId { get; set; }
        [Required]
        [Column("tsdProjectDef")]
        [StringLength(9)]
        public string TsdProjectDef { get; set; }
        [Required]
        [Column("tsdnight")]
        [StringLength(5)]
        public string Tsdnight { get; set; }
        [Column("tsdDayNumber")]
        public byte TsdDayNumber { get; set; }
        [Column("tsdTimeIn", TypeName = "datetime")]
        public DateTime? TsdTimeIn { get; set; }
        [Column("tsdTimeOut", TypeName = "datetime")]
        public DateTime? TsdTimeOut { get; set; }
        [Column("tsdHrsDay")]
        public float? TsdHrsDay { get; set; }
        [Column("tsdAllwDelayTime")]
        public byte? TsdAllwDelayTime { get; set; }
        [Column("tsdLunchTimeFrom", TypeName = "datetime")]
        public DateTime? TsdLunchTimeFrom { get; set; }
        [Column("tsdLunchTimeTo", TypeName = "datetime")]
        public DateTime? TsdLunchTimeTo { get; set; }
        [Column("tsdLunchHrs")]
        public float? TsdLunchHrs { get; set; }
        [Column("tsdPrayTimeFrom", TypeName = "datetime")]
        public DateTime? TsdPrayTimeFrom { get; set; }
        [Column("tsdPrayTimeTo", TypeName = "datetime")]
        public DateTime? TsdPrayTimeTo { get; set; }
        [Column("tsdPrayHrs")]
        public float? TsdPrayHrs { get; set; }
        [Column("tsdWeekEnd")]
        public byte? TsdWeekEnd { get; set; }
    }
}
