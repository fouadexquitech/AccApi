using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpImportLabor")]
    public partial class TmpImportLabor
    {
        [Column("File no")]
        [StringLength(10)]
        public string FileNo { get; set; }
        [StringLength(255)]
        public string Sponsor { get; set; }
        [Column("First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("Father Name")]
        [StringLength(50)]
        public string FatherName { get; set; }
        [Column("Family Name")]
        [StringLength(50)]
        public string FamilyName { get; set; }
        [Column("Mother Name")]
        [StringLength(50)]
        public string MotherName { get; set; }
        [Column("Mother Family")]
        [StringLength(50)]
        public string MotherFamily { get; set; }
        [Column("First Name(Arabic)")]
        [StringLength(50)]
        public string FirstNameArabic { get; set; }
        [Column("Father Name(Arabic)")]
        [StringLength(50)]
        public string FatherNameArabic { get; set; }
        [Column("Family Name(Arabic)")]
        [StringLength(50)]
        public string FamilyNameArabic { get; set; }
        [Column("Mother Name(Arabic)")]
        [StringLength(50)]
        public string MotherNameArabic { get; set; }
        [Column("Mother Family(Arabic)")]
        [StringLength(50)]
        public string MotherFamilyArabic { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Dob { get; set; }
        [Column("Place of Birth")]
        [StringLength(50)]
        public string PlaceOfBirth { get; set; }
        [StringLength(100)]
        public string Governorate { get; set; }
        [Column("Hiring Date", TypeName = "datetime")]
        public DateTime? HiringDate { get; set; }
        [StringLength(255)]
        public string Occupation { get; set; }
        [StringLength(255)]
        public string Nationality { get; set; }
        [Column("Day fee")]
        [StringLength(255)]
        public string DayFee { get; set; }
        [Column("Other allow")]
        [StringLength(255)]
        public string OtherAllow { get; set; }
        [Column("Food allow")]
        [StringLength(255)]
        public string FoodAllow { get; set; }
        [Column("Transport allow")]
        [StringLength(255)]
        public string TransportAllow { get; set; }
        [Column("WE Pay Type")]
        [StringLength(255)]
        public string WePayType { get; set; }
        [Column("Excep Daily Working Hrs")]
        [StringLength(255)]
        public string ExcepDailyWorkingHrs { get; set; }
        [Column("Excep OT Hr Rate")]
        [StringLength(255)]
        public string ExcepOtHrRate { get; set; }
        [Column("OT Hr Rate")]
        [StringLength(255)]
        public string OtHrRate { get; set; }
        [Column("WE Hr Rate")]
        [StringLength(255)]
        public string WeHrRate { get; set; }
        [Column("Hol Hr Rate")]
        [StringLength(255)]
        public string HolHrRate { get; set; }
        [Column("ENTRY NO")]
        [StringLength(255)]
        public string EntryNo { get; set; }
        [StringLength(255)]
        public string Location { get; set; }
        [Column("National No")]
        [StringLength(255)]
        public string NationalNo { get; set; }
        [Column("Phone Number")]
        [StringLength(255)]
        public string PhoneNumber { get; set; }
        [Column("IQAMA NO#")]
        [StringLength(255)]
        public string IqamaNo { get; set; }
        [Column("Passport No")]
        [StringLength(255)]
        public string PassportNo { get; set; }
        [Column("labResidPlace")]
        [StringLength(100)]
        public string LabResidPlace { get; set; }
        [Column("Place of Residence")]
        [StringLength(100)]
        public string PlaceOfResidence { get; set; }
    }
}
