using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblImportLogFile")]
    public partial class TblImportLogFile
    {
        [Key]
        [Column("ilfSeq")]
        public int IlfSeq { get; set; }
        [Column("ilfType")]
        [StringLength(25)]
        public string IlfType { get; set; }
        [Column("ilfDateTime", TypeName = "datetime")]
        public DateTime? IlfDateTime { get; set; }
        [Column("ilfUser")]
        [StringLength(10)]
        public string IlfUser { get; set; }
        [Column("ilfWeekFrom")]
        public int? IlfWeekFrom { get; set; }
        [Column("ilfWeekTo")]
        public int? IlfWeekTo { get; set; }
    }
}
