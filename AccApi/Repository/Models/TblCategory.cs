using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblCategory")]
    public partial class TblCategory
    {
        [Key]
        [Column("ctgSeq")]
        public byte CtgSeq { get; set; }
        [Required]
        [Column("ctgDesc")]
        [StringLength(200)]
        public string CtgDesc { get; set; }
        [Column("ctgAbv")]
        [StringLength(2)]
        public string CtgAbv { get; set; }
        [Column("ctgInDirect")]
        public byte? CtgInDirect { get; set; }
    }
}
