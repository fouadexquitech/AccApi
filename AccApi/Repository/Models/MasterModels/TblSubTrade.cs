using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblSubTrade")]
    public partial class TblSubTrade
    {
        [Key]
        [Column("stSeq")]
        public int StSeq { get; set; }
        [Column("stTradeSeq")]
        public int? StTradeSeq { get; set; }
        [Column("stProj")]
        public int? StProj { get; set; }
        [Column("stDivCode")]
        [StringLength(5)]
        public string StDivCode { get; set; }
        [Column("stSubDivCode")]
        [StringLength(5)]
        public string StSubDivCode { get; set; }
        [Column("stTradeCode")]
        [StringLength(50)]
        public string StTradeCode { get; set; }
        [Column("stSubTradeCode")]
        [StringLength(5)]
        public string StSubTradeCode { get; set; }
        [Column("stDivTradeCode")]
        [StringLength(10)]
        public string StDivTradeCode { get; set; }
        [Column("stSubTradeDesc")]
        [StringLength(100)]
        public string StSubTradeDesc { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("stUsage")]
        public byte? StUsage { get; set; }
    }
}
