using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblAuditLog")]
    public partial class TblAuditLog
    {
        [Key]
        [Column("seq")]
        public long Seq { get; set; }
        [Column("tablename")]
        [StringLength(50)]
        public string Tablename { get; set; }
        [Column("userid")]
        [StringLength(50)]
        public string Userid { get; set; }
        [Column("datetime", TypeName = "datetime")]
        public DateTime? Datetime { get; set; }
        [Column("action")]
        [StringLength(500)]
        public string Action { get; set; }
        [Column("primarykeyvalue")]
        [StringLength(50)]
        public string Primarykeyvalue { get; set; }
    }
}
