using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesInDirectGroups")]
    public partial class TblResourcesInDirectGroup
    {
        [Key]
        [Column("rigSeq")]
        [StringLength(14)]
        public string RigSeq { get; set; }
        [Column("rigSubDiv")]
        public byte RigSubDiv { get; set; }
        [Column("rigDesc")]
        [StringLength(255)]
        public string RigDesc { get; set; }
        [Column("rigAbv")]
        [StringLength(3)]
        public string RigAbv { get; set; }
        [Column("rigOrder")]
        public short? RigOrder { get; set; }
    }
}
