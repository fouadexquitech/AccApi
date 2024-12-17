using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblRndSel")]
    [Index(nameof(RnsWho), Name = "IX_tblRndSel")]
    [Index(nameof(RnsUser), Name = "IX_tblRndSel_1")]
    [Index(nameof(RnsRnd), Name = "IX_tblRndSel_2")]
    public partial class TblRndSel
    {
        [Key]
        [Column("rnsSeq")]
        public long RnsSeq { get; set; }
        [Required]
        [Column("rnsRnd")]
        [StringLength(20)]
        public string RnsRnd { get; set; }
        [Column("rnsWho")]
        public short RnsWho { get; set; }
        [Required]
        [Column("rnsUser")]
        [StringLength(10)]
        public string RnsUser { get; set; }
        [Column("rnsCod")]
        [StringLength(50)]
        public string RnsCod { get; set; }
        [Column("rnsDsc")]
        [StringLength(255)]
        public string RnsDsc { get; set; }
        [Column("rnsSel")]
        public bool? RnsSel { get; set; }
        [Column("rnsSupCode")]
        public int? RnsSupCode { get; set; }
        [Column("rnsDate", TypeName = "datetime")]
        public DateTime? RnsDate { get; set; }
        [Column("rnsTime", TypeName = "datetime")]
        public DateTime? RnsTime { get; set; }
        [Column("rnsType")]
        public byte? RnsType { get; set; }
        [Column("rnsClass")]
        [StringLength(10)]
        public string RnsClass { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Dte { get; set; }
        [Column("rnsPreMRCode")]
        public int? RnsPreMrcode { get; set; }
        [Column("rnsWhsTo")]
        public int? RnsWhsTo { get; set; }
        [Column("rnsStatus")]
        public byte? RnsStatus { get; set; }
        [Column("rnsProject")]
        [StringLength(30)]
        public string RnsProject { get; set; }
    }
}
