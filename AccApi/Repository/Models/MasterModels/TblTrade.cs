using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblTrades")]
    public partial class TblTrade
    {
        [Key]
        [Column("trSeq")]
        public int TrSeq { get; set; }
        [Column("trAbv")]
        [StringLength(3)]
        public string TrAbv { get; set; }
        [Column("trAbvTitle")]
        [StringLength(5)]
        public string TrAbvTitle { get; set; }
        [Required]
        [Column("trTrade")]
        [StringLength(50)]
        public string TrTrade { get; set; }
        [Column("trSubContractor")]
        public int? TrSubContractor { get; set; }
        [StringLength(10)]
        public string InsertBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [StringLength(10)]
        public string LastUpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
        [Column("trDivCode")]
        [StringLength(5)]
        public string TrDivCode { get; set; }
        [Column("trSubDivCode")]
        [StringLength(5)]
        public string TrSubDivCode { get; set; }
        [Column("trTradeCode")]
        [StringLength(5)]
        public string TrTradeCode { get; set; }
        [Column("trDivSubDivTrade")]
        [StringLength(20)]
        public string TrDivSubDivTrade { get; set; }
        [Column("trSelected")]
        public byte? TrSelected { get; set; }
        [Column("trProjID")]
        public int? TrProjId { get; set; }
        [Column("trDisciplineGrp")]
        [StringLength(50)]
        public string TrDisciplineGrp { get; set; }
        [Column("trDisciplineSubGrp")]
        [StringLength(50)]
        public string TrDisciplineSubGrp { get; set; }
    }
}
