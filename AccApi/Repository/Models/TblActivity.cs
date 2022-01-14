using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblActivities")]
    public partial class TblActivity
    {
        [Key]
        [Column("actSeq")]
        public byte ActSeq { get; set; }
        [Column("actDesc")]
        [StringLength(30)]
        public string ActDesc { get; set; }
    }
}
