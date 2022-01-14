using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBudgetVolume")]
    public partial class TblBudgetVolume
    {
        [Key]
        [Column("bvDiv")]
        [StringLength(2)]
        public string BvDiv { get; set; }
        [Key]
        [Column("bvSubDiv")]
        [StringLength(3)]
        public string BvSubDiv { get; set; }
        [Key]
        [Column("bvTrade")]
        [StringLength(5)]
        public string BvTrade { get; set; }
        [Column("bvPer")]
        public double? BvPer { get; set; }
    }
}
