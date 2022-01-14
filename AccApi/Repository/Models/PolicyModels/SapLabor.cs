using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("SAP_Labors")]
    public partial class SapLabor
    {
        [Column("File No")]
        public double? FileNo { get; set; }
        [Column("First Name")]
        [StringLength(255)]
        public string FirstName { get; set; }
        [Column("Father Name")]
        [StringLength(255)]
        public string FatherName { get; set; }
        [Column("Family Name")]
        [StringLength(255)]
        public string FamilyName { get; set; }
        [Column("Hiring Date", TypeName = "datetime")]
        public DateTime? HiringDate { get; set; }
        [StringLength(255)]
        public string PositionNo { get; set; }
        [StringLength(255)]
        public string PositionNodesc { get; set; }
        [Key]
        [Column("manualLabID")]
        public int ManualLabId { get; set; }
        [Column("manualLabID13")]
        public int? ManualLabId13 { get; set; }
        [Column("manul")]
        public int Manul { get; set; }
    }
}
