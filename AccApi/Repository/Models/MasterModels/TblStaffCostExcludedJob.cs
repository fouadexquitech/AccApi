using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblStaffCost_ExcludedJob")]
    public partial class TblStaffCostExcludedJob
    {
        [Column("ejSeq")]
        public int EjSeq { get; set; }
        [Key]
        [Column("ejJob")]
        [StringLength(100)]
        public string EjJob { get; set; }
        [Column("ejCost")]
        public int? EjCost { get; set; }
    }
}
