using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTimeSchedule")]
    public partial class TblTimeSchedule
    {
        [Key]
        [Column("tsdProjID")]
        public int TsdProjId { get; set; }
        [Key]
        [Column("tsdProjectDef")]
        [StringLength(20)]
        public string TsdProjectDef { get; set; }
        [Key]
        [Column("tsdnight")]
        [StringLength(5)]
        public string Tsdnight { get; set; }
        [Key]
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
        [Column("tsdWeekEndFrom", TypeName = "datetime")]
        public DateTime? TsdWeekEndFrom { get; set; }
        [Column("tsdWeekEndTo", TypeName = "datetime")]
        public DateTime? TsdWeekEndTo { get; set; }
    }
}
