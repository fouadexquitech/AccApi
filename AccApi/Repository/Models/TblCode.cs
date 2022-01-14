using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class TblCode
    {
        [Key]
        public int Seq { get; set; }
        [Column("codType")]
        public int? CodType { get; set; }
        [Column("codGroup")]
        public int? CodGroup { get; set; }
        [Column("codDescA")]
        [StringLength(100)]
        public string CodDescA { get; set; }
        [Column("codDescE")]
        [StringLength(100)]
        public string CodDescE { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("codColNo")]
        public byte? CodColNo { get; set; }
        public byte? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("codAbv")]
        [StringLength(10)]
        public string CodAbv { get; set; }
        [Column("codSpecialFilter")]
        [StringLength(50)]
        public string CodSpecialFilter { get; set; }
        [Column("codAuxiliaryCost")]
        public byte? CodAuxiliaryCost { get; set; }
        [Column("TSDB")]
        [StringLength(50)]
        public string Tsdb { get; set; }
    }
}
