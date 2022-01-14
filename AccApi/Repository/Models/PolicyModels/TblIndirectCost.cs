using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblIndirectCost")]
    public partial class TblIndirectCost
    {
        [Key]
        [Column("icArea")]
        public int IcArea { get; set; }
        [Key]
        [Column("icType")]
        public int IcType { get; set; }
        [Column("icYears")]
        public int? IcYears { get; set; }
        [Column("icCost")]
        public float? IcCost { get; set; }
        [Column("icMonthDays")]
        public int? IcMonthDays { get; set; }
        [Column("icDayHours")]
        public int? IcDayHours { get; set; }
        [Column("icHourCost")]
        public float? IcHourCost { get; set; }
    }
}
