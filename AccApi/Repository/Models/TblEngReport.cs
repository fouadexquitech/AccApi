using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblEngReport")]
    public partial class TblEngReport
    {
        [Key]
        [Column("erSeq")]
        public long ErSeq { get; set; }
        [StringLength(50)]
        public string Project { get; set; }
        public short? Week { get; set; }
        [StringLength(30)]
        public string SubDivision { get; set; }
        [StringLength(30)]
        public string Trade { get; set; }
        public double? Qty { get; set; }
        [StringLength(12)]
        public string Forman { get; set; }
        public int? Zone { get; set; }
        public int? Area { get; set; }
        [StringLength(12)]
        public string Block { get; set; }
        public double? Carpenter { get; set; }
        public double? SteelFixer { get; set; }
        public double? Mason { get; set; }
        public double? Plasterer { get; set; }
        public double? Tiler { get; set; }
        public double? Labour { get; set; }
        public int? OtherLabors { get; set; }
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        public bool? SubContractor { get; set; }
        [Column("SubID")]
        [StringLength(12)]
        public string SubId { get; set; }
        [Column("BOQ")]
        [StringLength(20)]
        public string Boq { get; set; }
        [Column("erDate", TypeName = "datetime")]
        public DateTime? ErDate { get; set; }
        [Column("erDiv")]
        [StringLength(30)]
        public string ErDiv { get; set; }
        [Column("erSubDiv")]
        [StringLength(30)]
        public string ErSubDiv { get; set; }
        [Column("erTrade")]
        [StringLength(30)]
        public string ErTrade { get; set; }
        [Column("erPrice")]
        public double? ErPrice { get; set; }
        [Column("erDayNight")]
        public bool? ErDayNight { get; set; }
        [Column("erSecEng")]
        [StringLength(12)]
        public string ErSecEng { get; set; }
        [Column("LUser")]
        [StringLength(30)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column("erReadyMixHdr")]
        [StringLength(19)]
        public string ErReadyMixHdr { get; set; }
        [Column("erDwgNb")]
        [StringLength(100)]
        public string ErDwgNb { get; set; }
        [Column("erElements")]
        public int? ErElements { get; set; }
        [Column("erPlannedQty")]
        public double? ErPlannedQty { get; set; }
        [Column("erPlannedCarp")]
        public int? ErPlannedCarp { get; set; }
        [Column("erPlannedSteelfix")]
        public int? ErPlannedSteelfix { get; set; }
        [Column("erPlannedMC")]
        public int? ErPlannedMc { get; set; }
        [Column("erPlannedLabor")]
        public int? ErPlannedLabor { get; set; }
        [Column("erPlannedOtherLabors")]
        public int? ErPlannedOtherLabors { get; set; }
        [Column("erCounter")]
        public int? ErCounter { get; set; }
        public int? SubArea { get; set; }
        [Column("erConManConf")]
        public bool? ErConManConf { get; set; }
        [Column("erSelect")]
        public bool? ErSelect { get; set; }
        [Column("erSelectUser")]
        [StringLength(15)]
        public string ErSelectUser { get; set; }
        [Column("isTemplate")]
        public bool? IsTemplate { get; set; }
        [Column("erSiteEng")]
        public int? ErSiteEng { get; set; }
        [Column("erPlannedCast")]
        public int? ErPlannedCast { get; set; }
        [Column("erPlannedTiler")]
        public int? ErPlannedTiler { get; set; }
        [Column("erPlannedPainter")]
        public int? ErPlannedPainter { get; set; }
        public int? Painter { get; set; }
        public int? Casting { get; set; }
        [Column("erActSubWBS")]
        public int? ErActSubWbs { get; set; }
        [StringLength(20)]
        public string PlannedInsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PlannedInsertedDate { get; set; }
    }
}
