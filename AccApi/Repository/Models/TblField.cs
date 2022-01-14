using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblFields")]
    public partial class TblField
    {
        [Key]
        [Column("fldTableName")]
        [StringLength(50)]
        public string FldTableName { get; set; }
        [Key]
        [Column("fldFieldName")]
        [StringLength(50)]
        public string FldFieldName { get; set; }
        [Required]
        [Column("fldFieldNameSub")]
        [StringLength(50)]
        public string FldFieldNameSub { get; set; }
        [Column("fldDesc")]
        [StringLength(50)]
        public string FldDesc { get; set; }
        [Column("fldAbv")]
        [StringLength(50)]
        public string FldAbv { get; set; }
        [Column("fldSort")]
        public byte? FldSort { get; set; }
        [Column("fldSrcTable")]
        [StringLength(50)]
        public string FldSrcTable { get; set; }
        [Column("fldSrcField")]
        [StringLength(50)]
        public string FldSrcField { get; set; }
        [Column("fldSrcKey")]
        [StringLength(50)]
        public string FldSrcKey { get; set; }
    }
}
