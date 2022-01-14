using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTransfPassport")]
    public partial class TblTransfPassport
    {
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [Column("PassportID")]
        [StringLength(50)]
        public string PassportId { get; set; }
        [Key]
        [Column("pasReceivBy")]
        [StringLength(20)]
        public string PasReceivBy { get; set; }
        [Key]
        [Column("pasReceivDate", TypeName = "datetime")]
        public DateTime PasReceivDate { get; set; }
        [Column("pasReceivLocation")]
        public int? PasReceivLocation { get; set; }
        [Column("pasTransfTo")]
        [StringLength(20)]
        public string PasTransfTo { get; set; }
        [Column("pasTransfDate", TypeName = "datetime")]
        public DateTime? PasTransfDate { get; set; }
        [Column("pasTransfLocation")]
        public int? PasTransfLocation { get; set; }
        [Column("pasNote")]
        [StringLength(200)]
        public string PasNote { get; set; }
        [StringLength(20)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [StringLength(20)]
        public string LuserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LdateUpdate { get; set; }
    }
}
