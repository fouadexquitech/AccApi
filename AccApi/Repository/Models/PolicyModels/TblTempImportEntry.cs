using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblTempImportEntry")]
    public partial class TblTempImportEntry
    {
        [Column("منفذ الدخول")]
        [StringLength(50)]
        public string منفذالدخول { get; set; }
        [StringLength(50)]
        public string الجنسية { get; set; }
        [StringLength(50)]
        public string الجنس { get; set; }
        [StringLength(50)]
        public string الاسم { get; set; }
        [Column("رقم دخول الحدود")]
        [StringLength(50)]
        public string رقمدخولالحدود { get; set; }
        public double? الرقم { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IqamaEndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaEndDate { get; set; }
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
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("تاريخ إنتهاء الإقامة")]
        [StringLength(50)]
        public string تاريخإنتهاءالإقامة { get; set; }
        [Column("تاريخ إنتهاء التأشيرة")]
        [StringLength(50)]
        public string تاريخإنتهاءالتأشيرة { get; set; }
        [Column("تاريخ الدخول")]
        [StringLength(50)]
        public string تاريخالدخول { get; set; }
        public bool? Staff { get; set; }
        [Column("FIRST NAMEA")]
        [StringLength(50)]
        public string FirstNamea { get; set; }
        [Column("Father NameA")]
        [StringLength(50)]
        public string FatherNameA { get; set; }
        [Column("FAMILY NAMEA")]
        [StringLength(50)]
        public string FamilyNamea { get; set; }
        [StringLength(50)]
        public string Occupation { get; set; }
        public double? DayFee { get; set; }
        public double? OtherAllow { get; set; }
        public double? FoodAllow { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HiringDate { get; set; }
        [StringLength(20)]
        public string Sponsor { get; set; }
        [StringLength(50)]
        public string Natio { get; set; }
    }
}
