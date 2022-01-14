using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSystemRevLog")]
    public partial class TblSystemRevLog
    {
        [Key]
        [Column("srlSeq")]
        public int SrlSeq { get; set; }
        [Required]
        [Column("srlRev")]
        [StringLength(11)]
        public string SrlRev { get; set; }
        [Column("srlDate", TypeName = "datetime")]
        public DateTime SrlDate { get; set; }
        [Column("srlLocation")]
        [StringLength(50)]
        public string SrlLocation { get; set; }
        [Column("srlUser")]
        [StringLength(50)]
        public string SrlUser { get; set; }
        [Column("srlRequestedBy")]
        [StringLength(50)]
        public string SrlRequestedBy { get; set; }
        [Column("srlNote")]
        [StringLength(100)]
        public string SrlNote { get; set; }
        [Column("srlInstalledOn", TypeName = "datetime")]
        public DateTime? SrlInstalledOn { get; set; }
        [Column("srlWorkBegan", TypeName = "datetime")]
        public DateTime? SrlWorkBegan { get; set; }
    }
}
