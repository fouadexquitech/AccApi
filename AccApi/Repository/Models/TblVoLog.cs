using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblVoLogs")]
    public partial class TblVoLog
    {
        [Column("voSeq")]
        public int VoSeq { get; set; }
        [Key]
        [Column("voRef")]
        [StringLength(50)]
        public string VoRef { get; set; }
        [Key]
        [Column("voProjCode")]
        [StringLength(20)]
        public string VoProjCode { get; set; }
        [Column("voDescription", TypeName = "text")]
        public string VoDescription { get; set; }
        [Column("voIsTimeImpact")]
        public bool? VoIsTimeImpact { get; set; }
        [Column("voType")]
        public byte? VoType { get; set; }
        [Column("voReasonAttributedTo")]
        public byte? VoReasonAttributedTo { get; set; }
        [Column("voReasonDate", TypeName = "date")]
        public DateTime? VoReasonDate { get; set; }
        [Column("voReasonRef")]
        [StringLength(200)]
        public string VoReasonRef { get; set; }
        [Column("voEngInstID")]
        [StringLength(200)]
        public string VoEngInstId { get; set; }
        [Column("voEngInstDate", TypeName = "date")]
        public DateTime? VoEngInstDate { get; set; }
        [Column("voEngInstRef")]
        [StringLength(200)]
        public string VoEngInstRef { get; set; }
        [Column("voContNotRef")]
        [StringLength(200)]
        public string VoContNotRef { get; set; }
        [Column("voContNotDate", TypeName = "date")]
        public DateTime? VoContNotDate { get; set; }
        [Column("voActBy")]
        public int? VoActBy { get; set; }
        [Column("voActTentTargetDate")]
        [StringLength(1000)]
        public string VoActTentTargetDate { get; set; }
        [Column("voActRefComments")]
        [StringLength(1000)]
        public string VoActRefComments { get; set; }
        [Column("voContSubmRef")]
        [StringLength(200)]
        public string VoContSubmRef { get; set; }
        [Column("voContSubmDate", TypeName = "date")]
        public DateTime? VoContSubmDate { get; set; }
        [Column("voContSubmAmt", TypeName = "money")]
        public decimal? VoContSubmAmt { get; set; }
        [Column("voContSubmRemark")]
        [StringLength(2000)]
        public string VoContSubmRemark { get; set; }
        [Column("voEngAssesRef")]
        [StringLength(200)]
        public string VoEngAssesRef { get; set; }
        [Column("voEngAssesDate", TypeName = "date")]
        public DateTime? VoEngAssesDate { get; set; }
        [Column("voEngAssesAmt", TypeName = "money")]
        public decimal? VoEngAssesAmt { get; set; }
        [Column("voEngAssesRemark")]
        [StringLength(2000)]
        public string VoEngAssesRemark { get; set; }
        [Column("voClientAssesRef")]
        [StringLength(200)]
        public string VoClientAssesRef { get; set; }
        [Column("voClientAssesDate", TypeName = "date")]
        public DateTime? VoClientAssesDate { get; set; }
        [Column("voClientAssesAmt", TypeName = "money")]
        public decimal? VoClientAssesAmt { get; set; }
        [Column("voClientAssesRemark")]
        [StringLength(2000)]
        public string VoClientAssesRemark { get; set; }
        [Column("voFinalAgreedRef")]
        [StringLength(200)]
        public string VoFinalAgreedRef { get; set; }
        [Column("voFinalAgreedDate", TypeName = "date")]
        public DateTime? VoFinalAgreedDate { get; set; }
        [Column("voFinalAgreedAmt", TypeName = "money")]
        public decimal? VoFinalAgreedAmt { get; set; }
        [Column("voProgAppPercAge")]
        public double? VoProgAppPercAge { get; set; }
        [Column("voProgAppAmt", TypeName = "money")]
        public decimal? VoProgAppAmt { get; set; }
        [Column("voProgCertPercAge")]
        public double? VoProgCertPercAge { get; set; }
        [Column("voProgCertAmt", TypeName = "money")]
        public decimal? VoProgCertAmt { get; set; }
        [Column("voProgMonth")]
        [StringLength(200)]
        public string VoProgMonth { get; set; }
        [Column("voClass")]
        [StringLength(50)]
        public string VoClass { get; set; }
    }
}
