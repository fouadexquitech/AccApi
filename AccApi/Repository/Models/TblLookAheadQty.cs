using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblLookAheadQty")]
    public partial class TblLookAheadQty
    {
        [Key]
        [Column("laSeq")]
        public long LaSeq { get; set; }
        [Column("laDate", TypeName = "datetime")]
        public DateTime? LaDate { get; set; }
        [Column("laWeek")]
        public short? LaWeek { get; set; }
        [Column("laProject")]
        [StringLength(10)]
        public string LaProject { get; set; }
        [StringLength(20)]
        public string Trade { get; set; }
        [Column("laTrade")]
        [StringLength(10)]
        public string LaTrade { get; set; }
        [Column("laDiv")]
        [StringLength(2)]
        public string LaDiv { get; set; }
        [Column("laSubDiv")]
        [StringLength(3)]
        public string LaSubDiv { get; set; }
        [Column("laActSubWBS")]
        public int? LaActSubWbs { get; set; }
        [Column("laForman")]
        public int? LaForman { get; set; }
        [Column("laZone")]
        public int? LaZone { get; set; }
        [Column("laArea")]
        public int? LaArea { get; set; }
        [Column("laSiteEng")]
        public int? LaSiteEng { get; set; }
        [Column("laSecEng")]
        public int? LaSecEng { get; set; }
        [Column("laDayNight")]
        public bool? LaDayNight { get; set; }
        [Column("laSubContractor")]
        [StringLength(100)]
        public string LaSubContractor { get; set; }
        [Column("laSubID")]
        [StringLength(12)]
        public string LaSubId { get; set; }
        [Column("laPlannedQty")]
        public double? LaPlannedQty { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
    }
}
