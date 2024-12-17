using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblPayments")]
    public partial class TblPayment
    {
        [Key]
        public int PayNb { get; set; }
        public int? Week { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [Column("payCertified")]
        public byte? PayCertified { get; set; }
        [Column("paySkip")]
        public byte? PaySkip { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public double? PayOverHead { get; set; }
        public int? PaymentType { get; set; }
        public double? PerformanceBondAmt { get; set; }
        [StringLength(200)]
        public string IssuedBy { get; set; }
        [StringLength(200)]
        public string GuaranteeNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApplicationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PeriodUpTo { get; set; }
        public string Note { get; set; }
        [Column("currency")]
        [StringLength(50)]
        public string Currency { get; set; }
        [Column(TypeName = "money")]
        public decimal? ContractValue { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValueEngineering { get; set; }
        [Column(TypeName = "money")]
        public decimal? PreviousVariations { get; set; }
        [Column(TypeName = "money")]
        public decimal? ApprovedVariationsThisMth { get; set; }
        [StringLength(50)]
        public string ContractType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartingDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletionDate { get; set; }
        public double? PercentTimeElapsed { get; set; }
        [Column(TypeName = "money")]
        public decimal? WorkDoneContract { get; set; }
        [Column(TypeName = "money")]
        public decimal? AmountValueEngineering { get; set; }
        [Column(TypeName = "money")]
        public decimal? WorkDoneVariation { get; set; }
        [Column(TypeName = "money")]
        public decimal? PlantandMaterialsOnSite { get; set; }
        [Column(TypeName = "money")]
        public decimal? PlantandMaterialsOffSite { get; set; }
        public double? LessRetention { get; set; }
        public double? ReleaseRetention { get; set; }
    }
}
