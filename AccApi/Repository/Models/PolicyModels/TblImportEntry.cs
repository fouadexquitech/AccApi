using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblImportEntry")]
    public partial class TblImportEntry
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [StringLength(50)]
        public string EntryNo { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [StringLength(50)]
        public string VisaEndDateHijri { get; set; }
        [StringLength(50)]
        public string IqamaEndDateHijri { get; set; }
        [StringLength(50)]
        public string Terminal { get; set; }
        [StringLength(50)]
        public string EntryDateHijri { get; set; }
        [StringLength(50)]
        public string Natio { get; set; }
        [StringLength(50)]
        public string Gender { get; set; }
        [StringLength(75)]
        public string Name { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IqamaEndDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? VisaEndDate { get; set; }
        [Column("FIRST NAME")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("Father Name")]
        [StringLength(50)]
        public string FatherName { get; set; }
        [Column("FAMILY NAME")]
        [StringLength(50)]
        public string FamilyName { get; set; }
        [Column("FIRST NAMEA")]
        [StringLength(50)]
        public string FirstNamea { get; set; }
        [Column("Father NameA")]
        [StringLength(50)]
        public string FatherNameA { get; set; }
        [Column("FAMILY NAMEA")]
        [StringLength(50)]
        public string FamilyNamea { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Dob { get; set; }
        public byte? EntryType { get; set; }
        public short? Progress { get; set; }
        [StringLength(20)]
        public string Sponsor { get; set; }
        public bool? Staff { get; set; }
        [StringLength(50)]
        public string Job { get; set; }
        [StringLength(50)]
        public string OccupVisa { get; set; }
        public double? DayFee { get; set; }
        public double? OtherAllow { get; set; }
        public double? FoodAllow { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        public bool? Exported { get; set; }
        [StringLength(15)]
        public string ExportedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExportedDate { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HiringDate { get; set; }
        [Column("disDeleted")]
        public byte? DisDeleted { get; set; }
        [Column("disDeletedBy")]
        [StringLength(10)]
        public string DisDeletedBy { get; set; }
        [Column("disDeletedOn", TypeName = "datetime")]
        public DateTime? DisDeletedOn { get; set; }
        public int? Religion { get; set; }
        public bool? Engineer { get; set; }
    }
}
