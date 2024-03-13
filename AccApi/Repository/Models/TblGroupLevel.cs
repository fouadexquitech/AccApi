using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblGroupLevel")]
    public partial class TblGroupLevel
    {
        [Key]
        [Column("levelSeq")]
        public int LevelSeq { get; set; }
        [Column("levelNo")]
        [StringLength(50)]
        public string LevelNo { get; set; }
        [Column("levelDesc")]
        [StringLength(5000)]
        public string LevelDesc { get; set; }
    }
}
