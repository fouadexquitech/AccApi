using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("TmpEngReport")]
    public partial class TmpEngReport
    {
        [Key]
        public int Seq { get; set; }
        [StringLength(50)]
        public string Project { get; set; }
        public short? Week { get; set; }
        [StringLength(30)]
        public string SubDivision { get; set; }
        [StringLength(30)]
        public string Trade { get; set; }
        [StringLength(75)]
        public string Forman { get; set; }
        [StringLength(50)]
        public string Zone { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [StringLength(50)]
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
        [StringLength(75)]
        public string SubContractor { get; set; }
        [Column("SubID")]
        [StringLength(75)]
        public string SubId { get; set; }
        [Column("BOQ")]
        [StringLength(20)]
        public string Boq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
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
        public bool? Night { get; set; }
        [StringLength(75)]
        public string SecEng { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [Column("Site Engineer")]
        [StringLength(75)]
        public string SiteEngineer { get; set; }
        public int? PlannedCarp { get; set; }
        public int? PlannedSteelFix { get; set; }
        public int? PlannedMason { get; set; }
        public int? PlannedLabor { get; set; }
        public int? PlannedTiler { get; set; }
        [Column("lannedPainter")]
        public int? LannedPainter { get; set; }
        public int? PlannedCasting { get; set; }
        [Column("plannedPainter")]
        public int? PlannedPainter { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Column("unit")]
        [StringLength(20)]
        public string Unit { get; set; }
        public double? ActualQty { get; set; }
        public double? PlannedQty { get; set; }
        [Column("SubWBS")]
        [StringLength(150)]
        public string SubWbs { get; set; }
        public double? TotalQty { get; set; }
    }
}
