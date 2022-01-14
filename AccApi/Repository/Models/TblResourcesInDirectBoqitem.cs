using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesInDirectBOQItem")]
    public partial class TblResourcesInDirectBoqitem
    {
        [Key]
        [Column("ribHdrSeq")]
        [StringLength(14)]
        public string RibHdrSeq { get; set; }
        [Key]
        [Column("ribCtg")]
        public byte RibCtg { get; set; }
        [Key]
        [Column("ribGrp")]
        [StringLength(14)]
        public string RibGrp { get; set; }
        [Key]
        [Column("ribBOQ")]
        [StringLength(25)]
        public string RibBoq { get; set; }
        [Column("ribPer")]
        public float? RibPer { get; set; }
    }
}
