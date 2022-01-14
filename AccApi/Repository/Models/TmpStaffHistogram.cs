using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tmpStaffHistogram")]
    public partial class TmpStaffHistogram
    {
        [Key]
        [Column("seq")]
        public long Seq { get; set; }
        [Key]
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        [StringLength(100)]
        public string SubCategory { get; set; }
        [StringLength(50)]
        public string Project { get; set; }
        public int? Revision { get; set; }
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public int? MthNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MthDate { get; set; }
        [Column("next_MthDate", TypeName = "datetime")]
        public DateTime? NextMthDate { get; set; }
        public double? PlanQty { get; set; }
        [Column("next_PlanQty")]
        public double? NextPlanQty { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? MthCurrent { get; set; }
        public int? MthDiff { get; set; }
        public double? ActQty { get; set; }
        public float? MonthlyCostPlan { get; set; }
        public double? PayrollCostPlan { get; set; }
        public double? OtherAllowCostPlan { get; set; }
        public double? TotalCostPlan { get; set; }
        public float? MonthlyCostAct { get; set; }
        public double? PayrollCostAct { get; set; }
        public double? OtherAllowCostAct { get; set; }
        public double? TotalCostAct { get; set; }
        [Column("cnt")]
        public int? Cnt { get; set; }
        public int? Sort { get; set; }
        public int? GrpSort { get; set; }
    }
}
