using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTemp")]
    public partial class TblTemp
    {
        [Key]
        [Column("seq")]
        public long Seq { get; set; }
        [StringLength(20)]
        public string UserName { get; set; }
        [Column("projDef")]
        [StringLength(20)]
        public string ProjDef { get; set; }
        [Column("didDate", TypeName = "datetime")]
        public DateTime? DidDate { get; set; }
        [StringLength(150)]
        public string Sponser { get; set; }
        [StringLength(100)]
        public string Zone { get; set; }
        [StringLength(100)]
        public string Job { get; set; }
        [Column(TypeName = "money")]
        public decimal? LaborsCount { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalHrs { get; set; }
        [Column("wbs")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [Column("wbsDesc")]
        [StringLength(100)]
        public string WbsDesc { get; set; }
        [Column("disArea")]
        [StringLength(50)]
        public string DisArea { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [StringLength(20)]
        public string Div { get; set; }
        [StringLength(100)]
        public string DivDesc { get; set; }
        public int? DynamicColCnt { get; set; }
        [StringLength(75)]
        public string Foreman { get; set; }
        [Column("insertDate", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [StringLength(5)]
        public string SubDiv { get; set; }
        [StringLength(100)]
        public string SubDivDesc { get; set; }
        [StringLength(10)]
        public string DirectCost { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(50)]
        public string Camp { get; set; }
        [StringLength(100)]
        public string HistogramJobDesc { get; set; }
        [Column("mpType")]
        public byte? MpType { get; set; }
        [Column("LabID")]
        [StringLength(10)]
        public string LabId { get; set; }
        [StringLength(50)]
        public string Nationality { get; set; }
        [StringLength(10)]
        public string MthName { get; set; }
        public int? DayNo { get; set; }
        [Column("disWE")]
        public byte? DisWe { get; set; }
        [Column("disHol")]
        public byte? DisHol { get; set; }
        [Column("isWEHol")]
        public byte? IsWehol { get; set; }
        [StringLength(200)]
        public string LabName { get; set; }
    }
}
