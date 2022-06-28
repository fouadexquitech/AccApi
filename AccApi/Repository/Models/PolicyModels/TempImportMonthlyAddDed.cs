using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("TempImportMonthlyAddDed")]
    public partial class TempImportMonthlyAddDed
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("LaborID")]
        [StringLength(10)]
        public string LaborId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public double? Amount { get; set; }
        [StringLength(1)]
        public string AddDedType { get; set; }
        [StringLength(255)]
        public string Remark { get; set; }
        [StringLength(30)]
        public string Project { get; set; }
        [StringLength(30)]
        public string ProjectDef { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate1 { get; set; }
    }
}
