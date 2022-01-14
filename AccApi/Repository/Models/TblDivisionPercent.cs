using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblDivisionPercent")]
    public partial class TblDivisionPercent
    {
        [Key]
        [Column("dpDiv")]
        [StringLength(2)]
        public string DpDiv { get; set; }
        [Key]
        [Column("dpFromLine")]
        public int DpFromLine { get; set; }
        [Key]
        [Column("dpToLine")]
        public int DpToLine { get; set; }
        [Column("dpL")]
        public float? DpL { get; set; }
        [Column("dpM")]
        public float? DpM { get; set; }
        [Column("dpE")]
        public float? DpE { get; set; }
        [Column("dpS")]
        public float? DpS { get; set; }
        [Column("dpO1")]
        public float? DpO1 { get; set; }
        [Column("dpO2")]
        public float? DpO2 { get; set; }
        [Column("dpO3")]
        public float? DpO3 { get; set; }
        [Column("dpTotal")]
        public float? DpTotal { get; set; }
        [Column("dpRemark")]
        [StringLength(100)]
        public string DpRemark { get; set; }
        [Column("dpQtyFactor")]
        public double? DpQtyFactor { get; set; }
        [Column("dpUnitRateFactor")]
        public double? DpUnitRateFactor { get; set; }
    }
}
