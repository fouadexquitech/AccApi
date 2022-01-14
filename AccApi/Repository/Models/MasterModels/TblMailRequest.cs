using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblMailRequest")]
    public partial class TblMailRequest
    {
        [Key]
        [Column("mlrReqSeq")]
        public long MlrReqSeq { get; set; }
        [Column("mlrProjCode")]
        [StringLength(50)]
        public string MlrProjCode { get; set; }
        [Column("mlrMailType")]
        [StringLength(20)]
        public string MlrMailType { get; set; }
        [Column("mlrCode")]
        [StringLength(30)]
        public string MlrCode { get; set; }
        [Column("mlrReqBy")]
        [StringLength(50)]
        public string MlrReqBy { get; set; }
        [Column("mlrRequesterMail")]
        [StringLength(50)]
        public string MlrRequesterMail { get; set; }
        [Column("mlrAttachment")]
        [StringLength(100)]
        public string MlrAttachment { get; set; }
        [Column("mlrSent")]
        public bool? MlrSent { get; set; }
        [Column("mlrSentOn", TypeName = "datetime")]
        public DateTime? MlrSentOn { get; set; }
        [Column("mlrSubject")]
        [StringLength(300)]
        public string MlrSubject { get; set; }
        [Column("mlrBody", TypeName = "ntext")]
        public string MlrBody { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("mlrWithTable")]
        public int? MlrWithTable { get; set; }
        [Column("mlrQueryOfTable", TypeName = "ntext")]
        public string MlrQueryOfTable { get; set; }
        [Column("mlrMailTo")]
        [StringLength(50)]
        public string MlrMailTo { get; set; }
    }
}
