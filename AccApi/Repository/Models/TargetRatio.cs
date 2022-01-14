using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class TargetRatio
    {
        [Key]
        [StringLength(50)]
        public string Trade { get; set; }
        [Key]
        [Column("trProject")]
        [StringLength(50)]
        public string TrProject { get; set; }
        [Column("trDescription")]
        [StringLength(50)]
        public string TrDescription { get; set; }
        [Column("trDivision")]
        [StringLength(50)]
        public string TrDivision { get; set; }
        [Column("trProductivity")]
        public double? TrProductivity { get; set; }
        [Column("trProdUnit")]
        [StringLength(50)]
        public string TrProdUnit { get; set; }
        [Column("trCost")]
        public double? TrCost { get; set; }
        [Column("trCostUnit")]
        [StringLength(50)]
        public string TrCostUnit { get; set; }
        [Column("trDiv")]
        [StringLength(3)]
        public string TrDiv { get; set; }
        [Column("trSubDiv")]
        [StringLength(3)]
        public string TrSubDiv { get; set; }
        [Column("trTrade")]
        [StringLength(5)]
        public string TrTrade { get; set; }
        [Column("trSubTrade")]
        [StringLength(3)]
        public string TrSubTrade { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("trQty")]
        public byte? TrQty { get; set; }
    }
}
