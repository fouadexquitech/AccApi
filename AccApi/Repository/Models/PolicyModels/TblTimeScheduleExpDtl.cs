using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTimeScheduleExpDtl")]
    public partial class TblTimeScheduleExpDtl
    {
        [Key]
        [Column("tsedHdrSeq")]
        public int TsedHdrSeq { get; set; }
        [Key]
        [Column("tsednight")]
        [StringLength(5)]
        public string Tsednight { get; set; }
        [Key]
        [Column("tsedDayNumber")]
        public byte TsedDayNumber { get; set; }
        [Column("tsedTimeIn", TypeName = "datetime")]
        public DateTime? TsedTimeIn { get; set; }
        [Column("tsedTimeOut", TypeName = "datetime")]
        public DateTime? TsedTimeOut { get; set; }
        [Column("tsedHrsDay")]
        public float? TsedHrsDay { get; set; }
        [Column("tsedAllwDelayTime")]
        public byte? TsedAllwDelayTime { get; set; }
        [Column("tsedLunchTimeFrom", TypeName = "datetime")]
        public DateTime? TsedLunchTimeFrom { get; set; }
        [Column("tsedLunchTimeTo", TypeName = "datetime")]
        public DateTime? TsedLunchTimeTo { get; set; }
        [Column("tsedLunchHrs")]
        public float? TsedLunchHrs { get; set; }
        [Column("tsedPrayTimeFrom", TypeName = "datetime")]
        public DateTime? TsedPrayTimeFrom { get; set; }
        [Column("tsedPrayTimeTo", TypeName = "datetime")]
        public DateTime? TsedPrayTimeTo { get; set; }
        [Column("tsedPrayHrs")]
        public float? TsedPrayHrs { get; set; }

        [ForeignKey(nameof(TsedHdrSeq))]
        [InverseProperty(nameof(TblTimeScheduleExpHdr.TblTimeScheduleExpDtls))]
        public virtual TblTimeScheduleExpHdr TsedHdrSeqNavigation { get; set; }
    }
}
