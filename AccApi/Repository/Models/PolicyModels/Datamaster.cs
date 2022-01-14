using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("DATAMaster")]
    public partial class Datamaster
    {
        [Key]
        public int Seq { get; set; }
        [StringLength(9)]
        public string F1 { get; set; }
        [StringLength(15)]
        public string F2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? F3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? F4 { get; set; }
        [StringLength(3)]
        public string F5 { get; set; }
        [StringLength(5)]
        public string F6 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? F7 { get; set; }
        public byte? Sts { get; set; }
        [Column("LUser")]
        [StringLength(20)]
        public string Luser { get; set; }
    }
}
