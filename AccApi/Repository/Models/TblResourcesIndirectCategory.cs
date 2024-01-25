using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesIndirectCategory")]
    public partial class TblResourcesIndirectCategory
    {
        [Key]
        [Column("ricSeq")]
        [StringLength(14)]
        public string RicSeq { get; set; }
        [Column("ricCtg")]
        public byte RicCtg { get; set; }
        [Required]
        [Column("ricDesc")]
        [StringLength(100)]
        public string RicDesc { get; set; }
        [Column("ricSort")]
        public int? RicSort { get; set; }
    }
}
