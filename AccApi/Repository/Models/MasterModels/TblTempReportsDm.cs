using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblTempReportsDMS")]
    public partial class TblTempReportsDm
    {
        [Key]
        [Column("SEQ")]
        public int Seq { get; set; }
        [Column("sdhForm")]
        public int? SdhForm { get; set; }
        [StringLength(20)]
        public string Abv { get; set; }
        [Column("SN")]
        public int? Sn { get; set; }
        [Column("sdhBldgs")]
        [StringLength(150)]
        public string SdhBldgs { get; set; }
        [Column("sdhElement")]
        public int? SdhElement { get; set; }
        [Column("ssdSeq")]
        [StringLength(10)]
        public string SsdSeq { get; set; }
        [Column("forSeq")]
        [StringLength(10)]
        public string ForSeq { get; set; }
        [Column("sdhSeq")]
        [StringLength(10)]
        public string SdhSeq { get; set; }
        [Column("sdhTradeCode")]
        [StringLength(30)]
        public string SdhTradeCode { get; set; }
        [StringLength(10)]
        public string Div { get; set; }
        [StringLength(100)]
        public string Submittal { get; set; }
        [Column("ssdRev")]
        [StringLength(50)]
        public string SsdRev { get; set; }
        [Column("adLoc1")]
        [StringLength(255)]
        public string AdLoc1 { get; set; }
        [Column("adLoc2")]
        [StringLength(255)]
        public string AdLoc2 { get; set; }
        [Column("adLoc3")]
        [StringLength(255)]
        public string AdLoc3 { get; set; }
        [Column("adLoc4")]
        [StringLength(255)]
        public string AdLoc4 { get; set; }
        [StringLength(100)]
        public string DwgNo { get; set; }
        [Column("forDwgDesc")]
        [StringLength(1500)]
        public string ForDwgDesc { get; set; }
        [Column("sdhRedesign")]
        public bool? SdhRedesign { get; set; }
        [StringLength(250)]
        public string Originator { get; set; }
        [StringLength(250)]
        public string Supplier { get; set; }
        [StringLength(250)]
        public string Agent { get; set; }
        [Column("ssdIssueDate", TypeName = "datetime")]
        public DateTime? SsdIssueDate { get; set; }
        [Column("ssdReturnDate", TypeName = "datetime")]
        public DateTime? SsdReturnDate { get; set; }
        [Column("forInspDate", TypeName = "datetime")]
        public DateTime? ForInspDate { get; set; }
        [Column("forInspTime", TypeName = "datetime")]
        public DateTime? ForInspTime { get; set; }
        [StringLength(50)]
        public string ReplyStatus { get; set; }
        [Column("forStatus")]
        public byte? ForStatus { get; set; }
        [Column("ssdEstReturnDate", TypeName = "datetime")]
        public DateTime? SsdEstReturnDate { get; set; }
        [Column("ssdEstReplyDuration")]
        public short? SsdEstReplyDuration { get; set; }
        public short? Duration { get; set; }
        [Column("ssdECC")]
        [StringLength(20)]
        public string SsdEcc { get; set; }
        public int? DelayDays { get; set; }
        public double? Diff { get; set; }
        [StringLength(100)]
        public string SecEng { get; set; }
        [StringLength(100)]
        public string SiteEng { get; set; }
        [StringLength(120)]
        public string Foreman { get; set; }
        [Column("forQualStatus")]
        public byte? ForQualStatus { get; set; }
        [StringLength(50)]
        public string Redesign { get; set; }
        [StringLength(150)]
        public string Attachment { get; set; }
        [Column("forRev")]
        public int? ForRev { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        public int? Srt { get; set; }
        [StringLength(50)]
        public string Validity { get; set; }
        [StringLength(50)]
        public string Revision { get; set; }
        public int? RowNo { get; set; }
        [Column("tradeDesc")]
        [StringLength(200)]
        public string TradeDesc { get; set; }
        [StringLength(250)]
        public string Location { get; set; }
        [Column("forBaselinStart", TypeName = "datetime")]
        public DateTime? ForBaselinStart { get; set; }
        [Column("forBaselinFinish", TypeName = "datetime")]
        public DateTime? ForBaselinFinish { get; set; }
        [Column("forBaselinConstStart", TypeName = "datetime")]
        public DateTime? ForBaselinConstStart { get; set; }
        [Column("forBaselinConstFinish", TypeName = "datetime")]
        public DateTime? ForBaselinConstFinish { get; set; }
        [StringLength(10)]
        public string RevNo { get; set; }
        [StringLength(150)]
        public string Discipline { get; set; }
        [Column("Villa Type")]
        [StringLength(50)]
        public string VillaType { get; set; }
        [StringLength(50)]
        public string Floor { get; set; }
        [StringLength(150)]
        public string DocumentNo { get; set; }
        [StringLength(2000)]
        public string Tittle { get; set; }
        [StringLength(150)]
        public string Manufacturer { get; set; }
        [StringLength(50)]
        public string Zone { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [Column("ssdCloseSubmDate", TypeName = "datetime")]
        public DateTime? SsdCloseSubmDate { get; set; }
        [StringLength(150)]
        public string DisciplineGrp { get; set; }
        [StringLength(150)]
        public string DisciplineSubGrp { get; set; }
        [StringLength(1000)]
        public string FormDesc { get; set; }
    }
}
