using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEntryPassport")]
    public partial class TblEntryPassport
    {
        [Key]
        [StringLength(50)]
        public string EntryNoHdr { get; set; }
        [Key]
        [Column("PassportID")]
        [StringLength(50)]
        public string PassportId { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("pasProdDate", TypeName = "datetime")]
        public DateTime? PasProdDate { get; set; }
        [Column("pasExpDate", TypeName = "datetime")]
        public DateTime? PasExpDate { get; set; }
        [Column("pasProdLocation")]
        public int? PasProdLocation { get; set; }
        [Column("pasReceivBy")]
        [StringLength(20)]
        public string PasReceivBy { get; set; }
        [Column("pasLocation")]
        public int? PasLocation { get; set; }
        [Column("pasNote")]
        [StringLength(200)]
        public string PasNote { get; set; }
        [Column("pasAttach")]
        [StringLength(150)]
        public string PasAttach { get; set; }
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
