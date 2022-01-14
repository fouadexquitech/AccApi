using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTrade")]
    public partial class TblTrade
    {
        [Key]
        public int Proj { get; set; }
        [Key]
        [StringLength(5)]
        public string Seq { get; set; }
        [Key]
        [StringLength(3)]
        public string Div { get; set; }
        [Key]
        [StringLength(3)]
        public string SubDiv { get; set; }
        [StringLength(100)]
        public string Trade { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public int? TrUnit { get; set; }
        public float? TrBudget { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public short? CalcQty { get; set; }
        [Column(TypeName = "money")]
        public decimal? Productivity { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }
        [Column("WBS")]
        [StringLength(50)]
        public string Wbs { get; set; }
        public byte? Used { get; set; }
        [Column("WBS_MAP")]
        [StringLength(8)]
        public string WbsMap { get; set; }
        [Column("CC")]
        [StringLength(8)]
        public string Cc { get; set; }
    }
}
