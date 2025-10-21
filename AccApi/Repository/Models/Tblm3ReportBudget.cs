using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblm3ReportBudget")]
    public partial class Tblm3ReportBudget
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("m3code")]
        [StringLength(50)]
        public string M3code { get; set; }
        [Column("m3Trade")]
        [StringLength(50)]
        public string M3Trade { get; set; }
        [Column("m3TradeDesc")]
        [StringLength(5000)]
        public string M3TradeDesc { get; set; }
        [Column("m3BudgetHrs")]
        public double? M3BudgetHrs { get; set; }
    }
}
