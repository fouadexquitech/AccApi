using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class EqpCostCode
    {
        [Key]
        [Column("ecSeq")]
        [StringLength(14)]
        public string EcSeq { get; set; }
        [Column("ecProj")]
        [StringLength(10)]
        public string EcProj { get; set; }
        [Column("ecCC")]
        [StringLength(15)]
        public string EcCc { get; set; }
        [Column("ecEqp")]
        public double? EcEqp { get; set; }
        [Column("ecDescription")]
        [StringLength(150)]
        public string EcDescription { get; set; }
        [Column("ecCostcenterDescription")]
        [StringLength(150)]
        public string EcCostcenterDescription { get; set; }
        [Column("ecTotal")]
        public double? EcTotal { get; set; }
        [Column("ecDate", TypeName = "datetime")]
        public DateTime? EcDate { get; set; }
        [Column("ecDiv")]
        [StringLength(3)]
        public string EcDiv { get; set; }
        [Column("ecSubDiv")]
        [StringLength(3)]
        public string EcSubDiv { get; set; }
        [Column("ecTrade")]
        [StringLength(5)]
        public string EcTrade { get; set; }
        [Column("ecSubTrade")]
        [StringLength(3)]
        public string EcSubTrade { get; set; }
        [Column("ecUpTo")]
        public int? EcUpTo { get; set; }
    }
}
