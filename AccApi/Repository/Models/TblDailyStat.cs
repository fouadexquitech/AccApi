using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblDailyStat")]
    public partial class TblDailyStat
    {
        [Required]
        [StringLength(14)]
        public string Seq { get; set; }
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labName")]
        [StringLength(75)]
        public string LabName { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        public short? Present { get; set; }
        public short? Absent { get; set; }
        public short? Accident { get; set; }
        public short? PaidVacation { get; set; }
        public short? NotPaidVacation { get; set; }
        public short? SickLeave { get; set; }
        public short? Holiday { get; set; }
        [Column("labFileNo")]
        [StringLength(50)]
        public string LabFileNo { get; set; }
    }
}
