using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSubTrade")]
    public partial class TblSubTrade
    {
        [Key]
        public int Proj { get; set; }
        [Key]
        [StringLength(3)]
        public string Seq { get; set; }
        [Key]
        [StringLength(3)]
        public string Div { get; set; }
        [Key]
        [StringLength(3)]
        public string SubDiv { get; set; }
        [Key]
        [StringLength(5)]
        public string Trade { get; set; }
        [StringLength(50)]
        public string SubTrade { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("stUsage")]
        public byte? StUsage { get; set; }
        [Column("WBS")]
        [StringLength(50)]
        public string Wbs { get; set; }
        public byte? Used { get; set; }
    }
}
