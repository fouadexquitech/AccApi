using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("AccountingCostCode")]
    public partial class AccountingCostCode
    {
        [Key]
        [Column("acProject")]
        [StringLength(10)]
        public string AcProject { get; set; }
        [Key]
        [Column("acUpto")]
        public int AcUpto { get; set; }
        [Key]
        [Column("acCC")]
        [StringLength(15)]
        public string AcCc { get; set; }
        [Key]
        [Column("acGLAccount")]
        [StringLength(50)]
        public string AcGlaccount { get; set; }
        [Column("acTotal")]
        public double? AcTotal { get; set; }
        [Column("acDiv")]
        [StringLength(2)]
        public string AcDiv { get; set; }
        [Column("acSubDiv")]
        [StringLength(3)]
        public string AcSubDiv { get; set; }
        [Column("acTrade")]
        [StringLength(5)]
        public string AcTrade { get; set; }
        [Column("acSubTrade")]
        [StringLength(5)]
        public string AcSubTrade { get; set; }
        [Column("WBS")]
        [StringLength(50)]
        public string Wbs { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("SAP")]
        public double? Sap { get; set; }
        public double? Adjustment { get; set; }
        public double? CostPlusRate { get; set; }
    }
}
