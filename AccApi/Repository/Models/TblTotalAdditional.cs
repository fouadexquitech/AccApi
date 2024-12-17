using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblTotalAdditional")]
    public partial class TblTotalAdditional
    {
        [Key]
        [Column("taSeq")]
        [StringLength(14)]
        public string TaSeq { get; set; }
        [Required]
        [StringLength(10)]
        public string Project { get; set; }
        [Column("week")]
        public int Week { get; set; }
        [Column("cc")]
        [StringLength(15)]
        public string Cc { get; set; }
        public int? Amount { get; set; }
        [StringLength(255)]
        public string Remarks { get; set; }
        [Column("taType")]
        public byte? TaType { get; set; }
        [Column("taLab", TypeName = "money")]
        public decimal? TaLab { get; set; }
        [Column("taMAt", TypeName = "money")]
        public decimal? TaMat { get; set; }
        [Column("taSubC", TypeName = "money")]
        public decimal? TaSubC { get; set; }
        [Column("taEqp", TypeName = "money")]
        public decimal? TaEqp { get; set; }
        [Column("taDiv")]
        [StringLength(2)]
        public string TaDiv { get; set; }
        [Column("taSubDiv")]
        [StringLength(3)]
        public string TaSubDiv { get; set; }
        [Column("taAbv")]
        [StringLength(3)]
        public string TaAbv { get; set; }
        [Column("taSkip")]
        public bool? TaSkip { get; set; }
        [Column("taOthers", TypeName = "money")]
        public decimal? TaOthers { get; set; }
        [Column("taPhase")]
        public int? TaPhase { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("taTrade")]
        [StringLength(5)]
        public string TaTrade { get; set; }
        [Column("taSkipTotPayment")]
        public int? TaSkipTotPayment { get; set; }
        [Column("taAddTotBudget")]
        public bool? TaAddTotBudget { get; set; }
        [Column("taVORefId")]
        public int? TaVorefId { get; set; }
    }
}
