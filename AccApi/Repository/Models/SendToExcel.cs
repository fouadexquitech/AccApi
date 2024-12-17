using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("SendToExcel")]
    public partial class SendToExcel
    {
        [Column("lcWeek")]
        [StringLength(50)]
        public string LcWeek { get; set; }
        [Column("lcDiv")]
        [StringLength(50)]
        public string LcDiv { get; set; }
        [Column("lcSubDiv")]
        [StringLength(50)]
        public string LcSubDiv { get; set; }
        [Column("lcTrade")]
        [StringLength(50)]
        public string LcTrade { get; set; }
        [Column("lcSubTrade")]
        [StringLength(50)]
        public string LcSubTrade { get; set; }
        [Column("lcCC")]
        [StringLength(50)]
        public string LcCc { get; set; }
        [Column("lcArea")]
        [StringLength(50)]
        public string LcArea { get; set; }
        [Column("lcForman")]
        [StringLength(50)]
        public string LcForman { get; set; }
        [Column("lcTotalCost")]
        [StringLength(50)]
        public string LcTotalCost { get; set; }
        [Column("lcTotalIndirectCost")]
        [StringLength(50)]
        public string LcTotalIndirectCost { get; set; }
        [Column("lcTotalHours")]
        [StringLength(50)]
        public string LcTotalHours { get; set; }
        [Column("lcDate", TypeName = "datetime")]
        public DateTime? LcDate { get; set; }
        [Column("lcSubCon")]
        [StringLength(200)]
        public string LcSubCon { get; set; }
        [Column("lcSiteEng")]
        [StringLength(500)]
        public string LcSiteEng { get; set; }
        [Column("lcSecEng")]
        [StringLength(200)]
        public string LcSecEng { get; set; }
        [Column("lcCampTranspAuxCost")]
        public double? LcCampTranspAuxCost { get; set; }
        [Column("lcIdleHours")]
        public double? LcIdleHours { get; set; }
        [Column("lcIdleCost")]
        public double? LcIdleCost { get; set; }
    }
}
