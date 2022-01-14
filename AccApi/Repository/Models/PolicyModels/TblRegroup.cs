using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("TblRegroup")]
    public partial class TblRegroup
    {
        [Key]
        public int Seq { get; set; }
        [Column("regRep")]
        [StringLength(20)]
        public string RegRep { get; set; }
        [Column("regRepCol")]
        public short? RegRepCol { get; set; }
        [Column("regRepList")]
        public bool? RegRepList { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
