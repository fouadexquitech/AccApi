using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblPayrollDates")]
    public partial class TblPayrollDate
    {
        [Key]
        [Column("pdPayrollNum")]
        public byte PdPayrollNum { get; set; }
        [Column("pdDateFrom", TypeName = "datetime")]
        public DateTime PdDateFrom { get; set; }
        [Column("pdDateTo", TypeName = "datetime")]
        public DateTime PdDateTo { get; set; }
        [Column("pdIssued")]
        public byte? PdIssued { get; set; }
        [Column("pdDateFromSS", TypeName = "datetime")]
        public DateTime? PdDateFromSs { get; set; }
        [Column("pdDateToSS", TypeName = "datetime")]
        public DateTime? PdDateToSs { get; set; }
        public byte? CalcLaborDailyWithAllowances { get; set; }
    }
}
