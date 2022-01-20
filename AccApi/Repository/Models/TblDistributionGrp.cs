using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblDistributionGrp")]
    public partial class TblDistributionGrp
    {
        [Key]
        [Column("gdSeq")]
        public int GdSeq { get; set; }
        [Column("gdDesc")]
        [StringLength(100)]
        public string GdDesc { get; set; }
        [Column("gdProject")]
        public int? GdProject { get; set; }
        [Column("gdEmail")]
        [StringLength(50)]
        public string GdEmail { get; set; }
    }
}
