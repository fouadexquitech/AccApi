using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblTempReportsDMS")]
    [Index(nameof(UserName), Name = "IX_tblTempReportsDMS")]
    [Index(nameof(SdhForm), Name = "IX_tblTempReportsDMS_1")]
    [Index(nameof(Validity), Name = "IX_tblTempReportsDMS_2")]
    [Index(nameof(AllUsers), Name = "IX_tblTempReportsDMS_3")]
    public partial class TblTempReportsDm
    {
        [Key]
        [Column("SEQ")]
        public long Seq { get; set; }
        [Column("sdhForm")]
        public int? SdhForm { get; set; }
        [StringLength(20)]
        public string Abv { get; set; }
        [Column("SN")]
        public int? Sn { get; set; }
        [Column("sdhBldgs")]
        [StringLength(350)]
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
        [StringLength(50)]
        public string SdhTradeCode { get; set; }
        [StringLength(10)]
        public string Div { get; set; }
        [StringLength(350)]
        public string Submittal { get; set; }
        [Column("ssdRev")]
        [StringLength(50)]
        public string SsdRev { get; set; }
        [Column("adLoc1")]
        [StringLength(350)]
        public string AdLoc1 { get; set; }
        [Column("adLoc2")]
        [StringLength(350)]
        public string AdLoc2 { get; set; }
        [Column("adLoc3")]
        [StringLength(350)]
        public string AdLoc3 { get; set; }
        [Column("adLoc4")]
        [StringLength(350)]
        public string AdLoc4 { get; set; }
        [StringLength(250)]
        public string DwgNo { get; set; }
        [Column("forDwgDesc")]
        public string ForDwgDesc { get; set; }
        [Column("sdhRedesign")]
        public bool? SdhRedesign { get; set; }
        [StringLength(350)]
        public string Originator { get; set; }
        [StringLength(350)]
        public string Supplier { get; set; }
        [StringLength(350)]
        public string Agent { get; set; }
        [Column("ssdIssueDate", TypeName = "datetime")]
        public DateTime? SsdIssueDate { get; set; }
        [Column("ssdReturnDate", TypeName = "datetime")]
        public DateTime? SsdReturnDate { get; set; }
        [Column("forInspDate", TypeName = "datetime")]
        public DateTime? ForInspDate { get; set; }
        [Column("forInspTime", TypeName = "datetime")]
        public DateTime? ForInspTime { get; set; }
        [StringLength(350)]
        public string ReplyStatus { get; set; }
        [Column("forStatus")]
        public byte? ForStatus { get; set; }
        [Column("ssdEstReturnDate", TypeName = "datetime")]
        public DateTime? SsdEstReturnDate { get; set; }
        [Column("ssdEstReplyDuration")]
        public short? SsdEstReplyDuration { get; set; }
        public short? Duration { get; set; }
        [Column("ssdECC")]
        [StringLength(350)]
        public string SsdEcc { get; set; }
        public int? DelayDays { get; set; }
        public double? Diff { get; set; }
        [StringLength(350)]
        public string SecEng { get; set; }
        [StringLength(350)]
        public string SiteEng { get; set; }
        [StringLength(350)]
        public string Foreman { get; set; }
        [Column("forQualStatus")]
        public byte? ForQualStatus { get; set; }
        [StringLength(50)]
        public string Redesign { get; set; }
        public string Attachment { get; set; }
        [Column("forRev")]
        public int? ForRev { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        public int? Srt { get; set; }
        [StringLength(350)]
        public string Validity { get; set; }
        [StringLength(50)]
        public string Revision { get; set; }
        public int? RowNo { get; set; }
        [Column("tradeDesc")]
        [StringLength(350)]
        public string TradeDesc { get; set; }
        [StringLength(350)]
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
        [StringLength(350)]
        public string Discipline { get; set; }
        [Column("Villa Type")]
        [StringLength(350)]
        public string VillaType { get; set; }
        [StringLength(350)]
        public string Floor { get; set; }
        [StringLength(350)]
        public string DocumentNo { get; set; }
        public string Tittle { get; set; }
        [StringLength(350)]
        public string Manufacturer { get; set; }
        [StringLength(350)]
        public string Zone { get; set; }
        [StringLength(350)]
        public string Area { get; set; }
        [Column("ssdCloseSubmDate", TypeName = "datetime")]
        public DateTime? SsdCloseSubmDate { get; set; }
        [StringLength(350)]
        public string DisciplineGrp { get; set; }
        [StringLength(350)]
        public string DisciplineSubGrp { get; set; }
        [StringLength(1000)]
        public string FormDesc { get; set; }
        [Column("ssdSubmDateCPA", TypeName = "datetime")]
        public DateTime? SsdSubmDateCpa { get; set; }
        [Column("ssdReturnDateCPA", TypeName = "datetime")]
        public DateTime? SsdReturnDateCpa { get; set; }
        [Column("ssdReplyStatusCPA")]
        [StringLength(350)]
        public string SsdReplyStatusCpa { get; set; }
        [Column("ReplyStatusCPA")]
        public int? ReplyStatusCpa { get; set; }
        [Column("sdhDate", TypeName = "datetime")]
        public DateTime? SdhDate { get; set; }
        public byte? AllUsers { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [Column("ProjectDB")]
        [StringLength(100)]
        public string ProjectDb { get; set; }
    }
}
