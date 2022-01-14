using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblWeeklyFormanByArea")]
    public partial class TblWeeklyFormanByArea
    {
        [Key]
        [Column("wfaWeek")]
        public int WfaWeek { get; set; }
        [Key]
        [Column("wfaForman")]
        [StringLength(12)]
        public string WfaForman { get; set; }
        [Key]
        [Column("wfaArea")]
        [StringLength(12)]
        public string WfaArea { get; set; }
        [Key]
        [Column("wfaTrade")]
        [StringLength(10)]
        public string WfaTrade { get; set; }
        [Column("wfaGenForman")]
        [StringLength(12)]
        public string WfaGenForman { get; set; }
        [Column("wfaTotalCost", TypeName = "money")]
        public decimal? WfaTotalCost { get; set; }
        [Column("wfaDiv")]
        [StringLength(2)]
        public string WfaDiv { get; set; }
        [Column("wfaSubDiv")]
        [StringLength(3)]
        public string WfaSubDiv { get; set; }
        [Column("wfaTradeCode")]
        [StringLength(5)]
        public string WfaTradeCode { get; set; }
    }
}
