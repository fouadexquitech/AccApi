using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInsBeneficiary")]
    public partial class TblInsBeneficiary
    {
        [Key]
        [Column("benId")]
        public int BenId { get; set; }
        [Column("sapId")]
        [StringLength(8)]
        public string SapId { get; set; }
        [Column("principalName")]
        [StringLength(250)]
        public string PrincipalName { get; set; }
        [Column("beneficiaryName")]
        [StringLength(250)]
        public string BeneficiaryName { get; set; }
        [Column("staffDaily")]
        [StringLength(50)]
        public string StaffDaily { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime? Dob { get; set; }
        [Column("age")]
        public int? Age { get; set; }
        [Column("dependency")]
        [StringLength(50)]
        public string Dependency { get; set; }
        [Column("relative")]
        [StringLength(50)]
        public string Relative { get; set; }
        [Column("gender")]
        [StringLength(50)]
        public string Gender { get; set; }
        [Column("nationality")]
        public int? Nationality { get; set; }
        [Column("emiratesId")]
        [StringLength(50)]
        public string EmiratesId { get; set; }
        [Column("passportId")]
        [StringLength(50)]
        public string PassportId { get; set; }
        [Column("unifiedNo_UID")]
        [StringLength(50)]
        public string UnifiedNoUid { get; set; }
        [Column("visaFileNumber")]
        [StringLength(50)]
        public string VisaFileNumber { get; set; }
    }
}
