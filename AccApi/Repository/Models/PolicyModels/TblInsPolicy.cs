using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInsPolicies")]
    public partial class TblInsPolicy
    {
        [Key]
        [Column("policySeq")]
        public int PolicySeq { get; set; }
        [Key]
        [Column("benId")]
        public int BenId { get; set; }
        [Column("transaction_TYPE")]
        public byte? TransactionType { get; set; }
        [Column("insurance_Status")]
        public byte? InsuranceStatus { get; set; }
        [Column("isuranceStartDATE", TypeName = "datetime")]
        public DateTime? IsuranceStartDate { get; set; }
        [Column("policyNo")]
        [StringLength(100)]
        public string PolicyNo { get; set; }
        [Column("visaEmpl")]
        public byte? VisaEmpl { get; set; }
        [Column("principal_CardNo")]
        [StringLength(50)]
        public string PrincipalCardNo { get; set; }
        [Column("beneficiary_CardNo")]
        [StringLength(50)]
        public string BeneficiaryCardNo { get; set; }
        [Column("age")]
        public double? Age { get; set; }
        [Column("age_Band")]
        public int? AgeBand { get; set; }
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
