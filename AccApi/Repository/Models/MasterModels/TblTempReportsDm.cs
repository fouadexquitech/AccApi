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
        [StringLength(150)]
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
        [StringLength(150)]
        public string SdhTradeCode { get; set; }
        [StringLength(10)]
        public string Div { get; set; }
        [StringLength(150)]
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
        public int? ForStatus { get; set; }
        [Column("ssdEstReturnDate", TypeName = "datetime")]
        public DateTime? SsdEstReturnDate { get; set; }
        [Column("ssdEstReplyDuration")]
        public int? SsdEstReplyDuration { get; set; }
        public int? Duration { get; set; }
        [Column("ssdECC")]
        public string SsdEcc { get; set; }
        public int? DelayDays { get; set; }
        public double? Diff { get; set; }
        public string SecEng { get; set; }
        public string SiteEng { get; set; }
        public string Foreman { get; set; }
        [Column("forQualStatus")]
        public int? ForQualStatus { get; set; }
        [StringLength(50)]
        public string Redesign { get; set; }
        public string Attachment { get; set; }
        [Column("forRev")]
        [StringLength(50)]
        public string ForRev { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        public int? Srt { get; set; }
        [StringLength(50)]
        public string Validity { get; set; }
        [StringLength(50)]
        public string Revision { get; set; }
        public int? RowNo { get; set; }
        [Column("tradeDesc")]
        public string TradeDesc { get; set; }
        public string Location { get; set; }
        [Column("forBaselinStart", TypeName = "datetime")]
        public DateTime? ForBaselinStart { get; set; }
        [Column("forBaselinFinish", TypeName = "datetime")]
        public DateTime? ForBaselinFinish { get; set; }
        [Column("forBaselinConstStart", TypeName = "datetime")]
        public DateTime? ForBaselinConstStart { get; set; }
        [Column("forBaselinConstFinish", TypeName = "datetime")]
        public DateTime? ForBaselinConstFinish { get; set; }
        [StringLength(50)]
        public string RevNo { get; set; }
        public string Discipline { get; set; }
        [Column("Villa Type")]
        public string VillaType { get; set; }
        public string Floor { get; set; }
        [StringLength(100)]
        public string DocumentNo { get; set; }
        public string Tittle { get; set; }
        [StringLength(100)]
        public string Manufacturer { get; set; }
        [StringLength(500)]
        public string Zone { get; set; }
        [StringLength(500)]
        public string Area { get; set; }
        [Column("ssdCloseSubmDate", TypeName = "datetime")]
        public DateTime? SsdCloseSubmDate { get; set; }
        [StringLength(50)]
        public string DisciplineGrp { get; set; }
        [StringLength(500)]
        public string FormDesc { get; set; }
        [StringLength(100)]
        public string DisciplineSubGrp { get; set; }
        [Column("ssdSubmDateCPA", TypeName = "datetime")]
        public DateTime? SsdSubmDateCpa { get; set; }
        [Column("ssdReturnDateCPA", TypeName = "datetime")]
        public DateTime? SsdReturnDateCpa { get; set; }
        [Column("ssdReplyStatusCPA")]
        [StringLength(50)]
        public string SsdReplyStatusCpa { get; set; }
        [Column("ReplyStatusCPA")]
        public int? ReplyStatusCpa { get; set; }
        [Column("sdhDate", TypeName = "datetime")]
        public DateTime? SdhDate { get; set; }
        public byte? AllUsers { get; set; }
        [Column("sdhProjectPhase")]
        [StringLength(50)]
        public string SdhProjectPhase { get; set; }
    }
}
