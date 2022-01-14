using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpChangeSalary")]
    public partial class TmpChangeSalary
    {
        [Column("LaborID")]
        [StringLength(10)]
        public string LaborId { get; set; }
        public double? Basic { get; set; }
        public double? Food { get; set; }
        public double? Transport { get; set; }
        public double? FixedMonthly { get; set; }
        public double? Housing { get; set; }
        public double? PhoneAllow { get; set; }
        [StringLength(250)]
        public string Occupation { get; set; }
        [StringLength(250)]
        public string Sponsor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column("WE Pay Type")]
        [StringLength(50)]
        public string WePayType { get; set; }
        [Column("Excep Daily Working Hrs")]
        [StringLength(50)]
        public string ExcepDailyWorkingHrs { get; set; }
        [Column("Excep OT Hr Rate")]
        [StringLength(50)]
        public string ExcepOtHrRate { get; set; }
        [Column("OT Hr Rate")]
        [StringLength(50)]
        public string OtHrRate { get; set; }
        [Column("WE Hr Rate")]
        [StringLength(50)]
        public string WeHrRate { get; set; }
        [Column("Hol Hr Rate")]
        [StringLength(50)]
        public string HolHrRate { get; set; }
    }
}
