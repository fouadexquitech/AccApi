using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblFacePunch")]
    public partial class TblFacePunch
    {
        [Key]
        [Column("seq")]
        public long Seq { get; set; }
        [StringLength(10)]
        public string ProjectCode { get; set; }
        [StringLength(10)]
        public string Id { get; set; }
        [StringLength(10)]
        public string AccId { get; set; }
        [StringLength(200)]
        public string LabName { get; set; }
        [StringLength(200)]
        public string Department { get; set; }
        [Column(TypeName = "date")]
        public DateTime? PunchDate { get; set; }
        [Column(TypeName = "time(5)")]
        public TimeSpan? FirstPunch { get; set; }
        [Column(TypeName = "time(5)")]
        public TimeSpan? LastPunch { get; set; }
        [Column(TypeName = "time(5)")]
        public TimeSpan? TotalTime { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [Column("isStaff")]
        public byte? IsStaff { get; set; }
        [StringLength(10)]
        public string DayNight { get; set; }
    }
}
