using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTrades")]
    public partial class TblTrade
    {
        [Key]
        [Column("trSeq")]
        public int TrSeq { get; set; }
        [Column("trProject")]
        [StringLength(9)]
        public string TrProject { get; set; }
        [Column("trDesc")]
        [StringLength(50)]
        public string TrDesc { get; set; }
    }
}
