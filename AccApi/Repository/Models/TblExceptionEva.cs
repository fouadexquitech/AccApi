using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblExceptionEVA")]
    public partial class TblExceptionEva
    {
        [Key]
        [Column("evaSeq")]
        [StringLength(12)]
        public string EvaSeq { get; set; }
        [Required]
        [Column("evaProject")]
        [StringLength(10)]
        public string EvaProject { get; set; }
        [Column("evaDiv")]
        [StringLength(2)]
        public string EvaDiv { get; set; }
        [Column("evaSubDiv")]
        [StringLength(3)]
        public string EvaSubDiv { get; set; }
        [Column("evaTrade")]
        [StringLength(5)]
        public string EvaTrade { get; set; }
        [Column("evaSubTrade")]
        [StringLength(3)]
        public string EvaSubTrade { get; set; }
        [Column("evaReport")]
        public byte? EvaReport { get; set; }
    }
}
