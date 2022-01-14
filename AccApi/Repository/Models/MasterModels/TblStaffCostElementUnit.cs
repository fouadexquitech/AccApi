using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblStaffCost_ElementUnit")]
    [Index(nameof(ScElement), nameof(ScUnit), Name = "IX_tblStaffCost_ElementUnit", IsUnique = true)]
    public partial class TblStaffCostElementUnit
    {
        [Key]
        public int Seq { get; set; }
        [Column("scElement")]
        [StringLength(100)]
        public string ScElement { get; set; }
        [Column("scUnit")]
        public int? ScUnit { get; set; }
    }
}
