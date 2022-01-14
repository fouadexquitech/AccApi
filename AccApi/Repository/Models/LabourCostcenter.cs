using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("LabourCostcenter")]
    public partial class LabourCostcenter
    {
        [Key]
        [Column("lcSeq")]
        public int LcSeq { get; set; }
        [Required]
        [Column("lcProj")]
        [StringLength(10)]
        public string LcProj { get; set; }
        [Column("lcWeek")]
        public short LcWeek { get; set; }
        [Required]
        [Column("lcCC")]
        [StringLength(30)]
        public string LcCc { get; set; }
        [Column("lcArea")]
        public int LcArea { get; set; }
        [Required]
        [Column("lcForman")]
        [StringLength(12)]
        public string LcForman { get; set; }
        [Column("lcHidden")]
        public byte LcHidden { get; set; }
        [Column("lcDayNight")]
        public byte LcDayNight { get; set; }
        [Required]
        [Column("lcOcc")]
        [StringLength(2)]
        public string LcOcc { get; set; }
        [Column("lcDate", TypeName = "datetime")]
        public DateTime? LcDate { get; set; }
        public bool Subcontractor { get; set; }
        [Column("LcWBS")]
        [StringLength(30)]
        public string LcWbs { get; set; }
        [Column("lcTotalCost")]
        public double? LcTotalCost { get; set; }
        [Column("lcTotalHours")]
        public double? LcTotalHours { get; set; }
        [Column("lcTotalCostWithAll")]
        public double? LcTotalCostWithAll { get; set; }
        [Column("lcDiv")]
        [StringLength(30)]
        public string LcDiv { get; set; }
        [Column("lcSubDiv")]
        [StringLength(30)]
        public string LcSubDiv { get; set; }
        [Column("lcTrade")]
        [StringLength(30)]
        public string LcTrade { get; set; }
        [Column("lcSubTrade")]
        [StringLength(30)]
        public string LcSubTrade { get; set; }
        [Column("lcSubCon")]
        public byte? LcSubCon { get; set; }
        [Column("lcGenForman")]
        [StringLength(12)]
        public string LcGenForman { get; set; }
        [Column("lcActivity")]
        public byte? LcActivity { get; set; }
        [Column("lcDivSubDiv")]
        [StringLength(30)]
        public string LcDivSubDiv { get; set; }
        [Column("lcDivTrade")]
        [StringLength(30)]
        public string LcDivTrade { get; set; }
        [Column("lcNote")]
        [StringLength(100)]
        public string LcNote { get; set; }
        [Column("lcSecEng")]
        [StringLength(12)]
        public string LcSecEng { get; set; }
        [Column("lcChecked")]
        public byte? LcChecked { get; set; }
        [Column("lcNorHrs")]
        public float? LcNorHrs { get; set; }
        [Column("lcOTHrs")]
        public float? LcOthrs { get; set; }
        [Column("lcNorPay")]
        public float? LcNorPay { get; set; }
        [Column("lcOTPay")]
        public float? LcOtpay { get; set; }
        [Column("lcLabCount")]
        public int? LcLabCount { get; set; }
        [Column("lcWorkDays")]
        public int? LcWorkDays { get; set; }
        [Column("lcHasQty")]
        public bool? LcHasQty { get; set; }
        [Column("lcTotalHours_NonProd")]
        public float? LcTotalHoursNonProd { get; set; }
        [Column("lcTotalCost_NonProd")]
        public float? LcTotalCostNonProd { get; set; }
        [Column("lcTotalIndirectCost")]
        public float? LcTotalIndirectCost { get; set; }
        [Column("lcCumulative")]
        public byte? LcCumulative { get; set; }
        [Column("LUser")]
        [StringLength(20)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("lcIdleCost")]
        public double? LcIdleCost { get; set; }
        [Column("lcIdleHours")]
        public double? LcIdleHours { get; set; }
        [Column("lcSiteEng")]
        public int? LcSiteEng { get; set; }
        [Column("lcCampTranspAuxCost")]
        public double? LcCampTranspAuxCost { get; set; }
        [Column("lcCarpenter")]
        public int? LcCarpenter { get; set; }
        [Column("lcSteelFixer")]
        public int? LcSteelFixer { get; set; }
        [Column("lcMason")]
        public int? LcMason { get; set; }
        [Column("lcLabour")]
        public int? LcLabour { get; set; }
        [Column("lcTiler")]
        public int? LcTiler { get; set; }
        [Column("lcPainter")]
        public int? LcPainter { get; set; }
        [Column("lcOtherLabors")]
        public int? LcOtherLabors { get; set; }
        [Column("lcPlasterer")]
        public int? LcPlasterer { get; set; }
    }
}
