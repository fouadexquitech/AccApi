using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblHolidays")]
    public partial class TblHoliday
    {
        [Key]
        [Column("holProjID")]
        public int HolProjId { get; set; }
        [Key]
        [Column("holDate", TypeName = "datetime")]
        public DateTime HolDate { get; set; }
        [Column("holDesc")]
        [StringLength(25)]
        public string HolDesc { get; set; }
        [Column("holDays")]
        public short? HolDays { get; set; }
        [Column("holYear")]
        public int? HolYear { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
    }
}
