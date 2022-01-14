using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDailyAbsentForman")]
    public partial class TblDailyAbsentForman
    {
        [Key]
        [Column("dafProject")]
        [StringLength(9)]
        public string DafProject { get; set; }
        [Key]
        [Column("dafDate", TypeName = "datetime")]
        public DateTime DafDate { get; set; }
        [Key]
        [Column("dafForman")]
        public int DafForman { get; set; }
        [Column("dafNote")]
        [StringLength(255)]
        public string DafNote { get; set; }
    }
}
