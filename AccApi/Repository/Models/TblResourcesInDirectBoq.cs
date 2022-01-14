using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesInDirectBOQ")]
    public partial class TblResourcesInDirectBoq
    {
        [Key]
        [Column("ribHdrSeq")]
        [StringLength(14)]
        public string RibHdrSeq { get; set; }
        [Key]
        [Column("ribBOQ")]
        [StringLength(25)]
        public string RibBoq { get; set; }
        [Column("ribPer")]
        public float? RibPer { get; set; }
    }
}
