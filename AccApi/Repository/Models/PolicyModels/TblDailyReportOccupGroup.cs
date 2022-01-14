using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDailyReportOccupGroup")]
    public partial class TblDailyReportOccupGroup
    {
        [Key]
        [Column("drProjID")]
        public int DrProjId { get; set; }
        [Key]
        [Column("drJobID")]
        public int DrJobId { get; set; }
        [Column("drJobGroup")]
        public int? DrJobGroup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("LUser")]
        [StringLength(20)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
    }
}
