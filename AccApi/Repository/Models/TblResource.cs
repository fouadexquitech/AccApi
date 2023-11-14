using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResources")]
    public partial class TblResource
    {
        [Key]
        [Column("resSeq")]
        [StringLength(14)]
        public string ResSeq { get; set; }
        [Column("resDescription")]
        [StringLength(255)]
        public string ResDescription { get; set; }
        [Column("resType")]
        public byte? ResType { get; set; }
        [Column("resSection")]
        [StringLength(3)]
        public string ResSection { get; set; }
        [Column("resUnit")]
        public int? ResUnit { get; set; }
        [Column("resUP")]
        public float? ResUp { get; set; }
        [Column("resUPCurr")]
        public float? ResUpcurr { get; set; }
        [Column("resCurr")]
        [StringLength(6)]
        public string ResCurr { get; set; }
        [Column("resWaste")]
        public float? ResWaste { get; set; }
        [Column("resRatioUnit")]
        public float? ResRatioUnit { get; set; }
        [Column("resProduction")]
        public float? ResProduction { get; set; }
        [Column("resRisk")]
        public float? ResRisk { get; set; }
        [Column("resSupplier")]
        [StringLength(12)]
        public string ResSupplier { get; set; }
        [Column("resNotes")]
        [StringLength(100)]
        public string ResNotes { get; set; }
        [Column("resSel")]
        public byte? ResSel { get; set; }
        [Column("resDiv")]
        [StringLength(2)]
        public string ResDiv { get; set; }
        [Column("resSubDiv")]
        [StringLength(3)]
        public string ResSubDiv { get; set; }
        [Column("resTrade")]
        [StringLength(2)]
        public string ResTrade { get; set; }
        [Column("resSubTrade")]
        [StringLength(3)]
        public string ResSubTrade { get; set; }
        public bool? IsSynched { get; set; }
    }
}
