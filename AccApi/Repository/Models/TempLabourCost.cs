using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("TempLabourCost")]
    public partial class TempLabourCost
    {
        [Required]
        [StringLength(10)]
        public string Proj { get; set; }
        public short Week { get; set; }
        [Required]
        [Column("CC")]
        [StringLength(15)]
        public string Cc { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        public int Area { get; set; }
        [Required]
        [StringLength(12)]
        public string Forman { get; set; }
        public byte Hidden { get; set; }
        public byte DayNight { get; set; }
        [Required]
        [StringLength(2)]
        public string Occ { get; set; }
        public double? TotalCost { get; set; }
        public double? TotalHours { get; set; }
        public double? TotalCostWithAll { get; set; }
        [StringLength(30)]
        public string Div { get; set; }
        [StringLength(30)]
        public string SubDiv { get; set; }
        [StringLength(30)]
        public string Trade { get; set; }
        [StringLength(30)]
        public string SubTrade { get; set; }
        public byte? SubCon { get; set; }
        [StringLength(12)]
        public string GenForman { get; set; }
        public byte? Activity { get; set; }
        [StringLength(30)]
        public string DivSubDiv { get; set; }
        [StringLength(30)]
        public string DivTrade { get; set; }
        [Column("txtNote")]
        [StringLength(100)]
        public string TxtNote { get; set; }
        [StringLength(12)]
        public string SecEng { get; set; }
        public byte? Checked { get; set; }
        public float? NorHrs { get; set; }
        [Column("OTHrs")]
        public float? Othrs { get; set; }
        public float? NorPay { get; set; }
        [Column("OTPay")]
        public float? Otpay { get; set; }
        public int? LabCount { get; set; }
        public int? WorkDays { get; set; }
        public bool? HasQty { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AttDate { get; set; }
        public int? SiteEng { get; set; }
    }
}
