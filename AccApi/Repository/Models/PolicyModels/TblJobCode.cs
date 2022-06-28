using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblJobCodes")]
    [Index(nameof(JcDesc), Name = "IX_tblJobCodes", IsUnique = true)]
    public partial class TblJobCode
    {
        [Key]
        [Column("jcSeq")]
        public int JcSeq { get; set; }
        [Column("jcCode")]
        [StringLength(10)]
        public string JcCode { get; set; }
        [Column("jcDesc")]
        [StringLength(100)]
        public string JcDesc { get; set; }
        [Column("jcDescAr")]
        [StringLength(50)]
        public string JcDescAr { get; set; }
        [StringLength(10)]
        public string Code { get; set; }
        [Column("codGrp")]
        public byte? CodGrp { get; set; }
        [Column("codSort")]
        public short? CodSort { get; set; }
        [Column("jcSkill")]
        public int? JcSkill { get; set; }
        [Column("jcShowDailyRpt")]
        [StringLength(50)]
        public string JcShowDailyRpt { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public short? InvoiceGroup { get; set; }
        [Column("jcRepCol")]
        public short? JcRepCol { get; set; }
        [Column("codShowOnAbsent")]
        public bool? CodShowOnAbsent { get; set; }
        [Column("codSubGrp")]
        public int? CodSubGrp { get; set; }
        [Column("grade")]
        [StringLength(20)]
        public string Grade { get; set; }
    }
}
