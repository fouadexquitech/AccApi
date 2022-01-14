using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblManpowerSuppSalary")]
    public partial class TblManpowerSuppSalary
    {
        [Key]
        [Column("mphID")]
        public int MphId { get; set; }
        [Key]
        [Column("mpClass")]
        [StringLength(3)]
        public string MpClass { get; set; }
        [Column("mpClassSalary")]
        public double? MpClassSalary { get; set; }
        [Column("mpClassSalaryCostSystem")]
        public double? MpClassSalaryCostSystem { get; set; }
        [Column("mpOtherAllowance")]
        public double? MpOtherAllowance { get; set; }
        [Column("mpOtherAllowanceCostSystem")]
        public double? MpOtherAllowanceCostSystem { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(10)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey(nameof(MphId))]
        [InverseProperty(nameof(TblManPowerSupp.TblManpowerSuppSalaries))]
        public virtual TblManPowerSupp Mph { get; set; }
    }
}
