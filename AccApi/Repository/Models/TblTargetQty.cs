using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblTargetQty")]
    public partial class TblTargetQty
    {
        [Key]
        [Column("tqSeq")]
        public int TqSeq { get; set; }
        [Required]
        [Column("tqTrade")]
        [StringLength(20)]
        public string TqTrade { get; set; }
        [Required]
        [Column("tqArea")]
        [StringLength(12)]
        public string TqArea { get; set; }
        [Column("tqProj")]
        [StringLength(20)]
        public string TqProj { get; set; }
        [Column("taMaxExecQty")]
        public double? TaMaxExecQty { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column("tqActSubWBS")]
        public int? TqActSubWbs { get; set; }
    }
}
