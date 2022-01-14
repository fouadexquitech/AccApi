using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class ViewSalary
    {
        [Column("File No")]
        [StringLength(10)]
        public string FileNo { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [Column("Daily Rate")]
        public double? DailyRate { get; set; }
        [Column("Working Days")]
        public double? WorkingDays { get; set; }
        [Column("Friday Days")]
        public double? FridayDays { get; set; }
        [Column("Total Days")]
        public double? TotalDays { get; set; }
        public double? Salary { get; set; }
        [Column("Sick Days")]
        public int? SickDays { get; set; }
        [Column("Sick Amount")]
        public double? SickAmount { get; set; }
        [Column("Monthly Allowance")]
        public double? MonthlyAllowance { get; set; }
        public double? Housing { get; set; }
        [Column("Normal  OT")]
        public double? NormalOt { get; set; }
        [Column("Bonus OT")]
        public double? BonusOt { get; set; }
        [Column("Holiday OT")]
        public double? HolidayOt { get; set; }
        [Column("Holiday Bonus OT")]
        public double? HolidayBonusOt { get; set; }
        [Column("Total OT")]
        public double? TotalOt { get; set; }
        [Column("OT Value")]
        public double? OtValue { get; set; }
        [Column("Gross Amount")]
        public double? GrossAmount { get; set; }
        [Column("Other Addition")]
        public double? OtherAddition { get; set; }
        [Column("Net Salary")]
        public double? NetSalary { get; set; }
    }
}
