using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("TmpExportEntry")]
    public partial class TmpExportEntry
    {
        [Column("File no")]
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("FIRST NAME")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("Father Name")]
        [StringLength(50)]
        public string FatherName { get; set; }
        [Column("FAMILY NAME")]
        [StringLength(50)]
        public string FamilyName { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Dob { get; set; }
        [Column("Hiring Date", TypeName = "datetime")]
        public DateTime? HiringDate { get; set; }
        [StringLength(50)]
        public string Occupation { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [Column("day fee")]
        public double? DayFee { get; set; }
        [Column("other allow")]
        public double? OtherAllow { get; set; }
        [Column("food allow")]
        public double? FoodAllow { get; set; }
        [Column("ENTRY NO")]
        [StringLength(50)]
        public string EntryNo { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        [Column("Passport Number")]
        [StringLength(50)]
        public string PassportNumber { get; set; }
        [StringLength(50)]
        public string Sponsor { get; set; }
        [Column("IQAMA NO.")]
        [StringLength(50)]
        public string IqamaNo { get; set; }
    }
}
