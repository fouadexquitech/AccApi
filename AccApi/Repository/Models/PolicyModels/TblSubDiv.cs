using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSubDiv")]
    public partial class TblSubDiv
    {
        [Key]
        public int Proj { get; set; }
        [Key]
        [StringLength(3)]
        public string Seq { get; set; }
        [Key]
        [StringLength(3)]
        public string HdrSeq { get; set; }
        [StringLength(50)]
        public string SubDiv { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("sdUnit")]
        public int? SdUnit { get; set; }
        [Column("sdBudget")]
        public float? SdBudget { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public byte? Used { get; set; }
    }
}
