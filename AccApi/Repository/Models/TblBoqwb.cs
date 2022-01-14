using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBOQWBS")]
    public partial class TblBoqwb
    {
        [Key]
        [Column("boqwRivision")]
        public short BoqwRivision { get; set; }
        [Key]
        [Column("boqwItem")]
        [StringLength(25)]
        public string BoqwItem { get; set; }
        [Key]
        [Column("boqwWBS")]
        [StringLength(50)]
        public string BoqwWbs { get; set; }
        [Key]
        [Column("boqwCtg")]
        [StringLength(1)]
        public string BoqwCtg { get; set; }
        [Key]
        [Column("boqwLevel")]
        [StringLength(5)]
        public string BoqwLevel { get; set; }
        [Key]
        [Column("boqWBackUpDate", TypeName = "datetime")]
        public DateTime BoqWbackUpDate { get; set; }
        [Column("boqwUnit")]
        [StringLength(10)]
        public string BoqwUnit { get; set; }
        [Column("boqwQty")]
        public double? BoqwQty { get; set; }
        [Column("boqwUPrice")]
        public double? BoqwUprice { get; set; }
        [Column("boqwNetPrice")]
        public float? BoqwNetPrice { get; set; }
        [Column("boqwSheetDesc")]
        [StringLength(50)]
        public string BoqwSheetDesc { get; set; }
        public int? RowNumber { get; set; }
        [Column("boqWProj")]
        [StringLength(50)]
        public string BoqWproj { get; set; }
    }
}
