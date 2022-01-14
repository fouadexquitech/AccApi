using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblReportsBy")]
    public partial class TblReportsBy
    {
        [Key]
        [Column("byRptSeq")]
        public byte ByRptSeq { get; set; }
        [Key]
        [Column("bySeq")]
        public byte BySeq { get; set; }
        [Column("byDesc")]
        [StringLength(30)]
        public string ByDesc { get; set; }
        [Column("byRptObject")]
        [StringLength(150)]
        public string ByRptObject { get; set; }
    }
}
