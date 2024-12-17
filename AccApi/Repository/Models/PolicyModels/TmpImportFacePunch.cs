using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpImportFacePunch")]
    public partial class TmpImportFacePunch
    {
        [Column("Employee ID")]
        [StringLength(100)]
        public string EmployeeId { get; set; }
        [Column("First Name")]
        [StringLength(1000)]
        public string FirstName { get; set; }
        [StringLength(500)]
        public string Department { get; set; }
        [StringLength(100)]
        public string Date { get; set; }
        [StringLength(100)]
        public string Weekday { get; set; }
        [Column("First Punch")]
        [StringLength(100)]
        public string FirstPunch { get; set; }
        [Column("Last Punch")]
        [StringLength(100)]
        public string LastPunch { get; set; }
        [Column("Total Time")]
        [StringLength(100)]
        public string TotalTime { get; set; }
        [StringLength(10)]
        public string AccId { get; set; }
        [Column("isStaff")]
        public byte? IsStaff { get; set; }
        [Column("Shift_")]
        [StringLength(10)]
        public string Shift { get; set; }
    }
}
