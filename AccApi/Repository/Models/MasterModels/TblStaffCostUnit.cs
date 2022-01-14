using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblStaffCost_Unit")]
    public partial class TblStaffCostUnit
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("scUnit")]
        [StringLength(75)]
        public string ScUnit { get; set; }
    }
}
