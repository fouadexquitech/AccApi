using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tmpWbsProgQty")]
    public partial class TmpWbsProgQty
    {
        [Key]
        public int Seq { get; set; }
        [Column("Mth No")]
        public int? MthNo { get; set; }
        [Column("WBS")]
        [StringLength(50)]
        public string Wbs { get; set; }
        [StringLength(50)]
        public string Level { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        [Column("Cum Progress Qty")]
        public double? CumProgressQty { get; set; }
        [Column("Subcontractor Progress Qty")]
        public double? SubcontractorProgressQty { get; set; }
        [Column("ACC Cum Progress Qty")]
        public double? AccCumProgressQty { get; set; }
        [Column("Subcontractor Cum Progress Qty")]
        public double? SubcontractorCumProgressQty { get; set; }
        [Column("BOQItem")]
        [StringLength(50)]
        public string Boqitem { get; set; }
    }
}
