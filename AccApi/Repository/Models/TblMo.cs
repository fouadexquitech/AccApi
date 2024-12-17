using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblMOS")]
    public partial class TblMo
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Required]
        [StringLength(10)]
        public string Project { get; set; }
        [Column("week")]
        public int Week { get; set; }
        [Required]
        [StringLength(50)]
        public string Item { get; set; }
        public string Description { get; set; }
        [StringLength(500)]
        public string Subcontractor { get; set; }
        [StringLength(500)]
        public string Invoice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InvDate { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        public double? QtyCont { get; set; }
        public double? QtyCons { get; set; }
        [Column("URate")]
        public double? Urate { get; set; }
        public double? AmtCum { get; set; }
        public double? AmtMat { get; set; }
        public double? AmtBalance { get; set; }
        public string Remarks { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("insertDate", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        public byte? OnOffSite { get; set; }
    }
}
