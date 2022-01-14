using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEntryResidence")]
    public partial class TblEntryResidence
    {
        [Key]
        [StringLength(50)]
        public string EntryNoHdr { get; set; }
        [Key]
        [Column("resID")]
        [StringLength(50)]
        public string ResId { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("resIssDate", TypeName = "datetime")]
        public DateTime? ResIssDate { get; set; }
        [Column("resExpDate", TypeName = "datetime")]
        public DateTime? ResExpDate { get; set; }
        [Column("resReceivBy")]
        [StringLength(20)]
        public string ResReceivBy { get; set; }
        [Column("resLocation")]
        public int? ResLocation { get; set; }
        [Column("resNote")]
        [StringLength(200)]
        public string ResNote { get; set; }
        [Column("resAttach")]
        [StringLength(150)]
        public string ResAttach { get; set; }
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
