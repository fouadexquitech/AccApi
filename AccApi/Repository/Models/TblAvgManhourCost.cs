using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblAvgManhourCost")]
    public partial class TblAvgManhourCost
    {
        [Key]
        [Column("amhProject")]
        [StringLength(10)]
        public string AmhProject { get; set; }
        [Key]
        [Column("amhWeek")]
        public short AmhWeek { get; set; }
        [Key]
        [Column("amhArea")]
        [StringLength(12)]
        public string AmhArea { get; set; }
        [Column("amhCarpenter")]
        public double? AmhCarpenter { get; set; }
        [Column("amhSteelFixer")]
        public double? AmhSteelFixer { get; set; }
        [Column("amhMason")]
        public double? AmhMason { get; set; }
        [Column("amhPlaster")]
        public double? AmhPlaster { get; set; }
        [Column("amhTiler")]
        public double? AmhTiler { get; set; }
        [Column("amhLabour")]
        public double? AmhLabour { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
