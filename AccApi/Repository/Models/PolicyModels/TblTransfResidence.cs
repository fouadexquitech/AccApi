using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTransfResidence")]
    public partial class TblTransfResidence
    {
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [Column("resID")]
        [StringLength(50)]
        public string ResId { get; set; }
        [Key]
        [Column("resReceivBy")]
        [StringLength(20)]
        public string ResReceivBy { get; set; }
        [Key]
        [Column("resReceivDate", TypeName = "datetime")]
        public DateTime ResReceivDate { get; set; }
        [Column("resReceivLocation")]
        public int? ResReceivLocation { get; set; }
        [Column("resTransfTo")]
        [StringLength(20)]
        public string ResTransfTo { get; set; }
        [Column("resTransfDate", TypeName = "datetime")]
        public DateTime? ResTransfDate { get; set; }
        [Column("resTransfLocation")]
        public int? ResTransfLocation { get; set; }
        [Column("resNote")]
        [StringLength(200)]
        public string ResNote { get; set; }
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
