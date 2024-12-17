using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblCodes")]
    public partial class TblCode
    {
        [Key]
        public int Seq { get; set; }
        [Column("codType")]
        public int? CodType { get; set; }
        [Column("codDescA")]
        [StringLength(100)]
        public string CodDescA { get; set; }
        [Column("codDescE")]
        [StringLength(100)]
        public string CodDescE { get; set; }
        [Column("codRep")]
        [StringLength(3)]
        public string CodRep { get; set; }
        [Column("codRepCol")]
        public short? CodRepCol { get; set; }
        [Column("code")]
        [StringLength(2)]
        public string Code { get; set; }
        [Column("codEst")]
        public int? CodEst { get; set; }
        [Column("codAccList")]
        public bool? CodAccList { get; set; }
        [Column("codRT")]
        [StringLength(1)]
        public string CodRt { get; set; }
        [Column("codRG")]
        public short? CodRg { get; set; }
        [Column("codCosts")]
        public double? CodCosts { get; set; }
        [Column("codSort")]
        public int? CodSort { get; set; }
        [Column("codMonthGrp")]
        public short? CodMonthGrp { get; set; }
        [Column("codEngRep")]
        public bool? CodEngRep { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("codColNo")]
        public short? CodColNo { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("codSel")]
        public bool? CodSel { get; set; }
        [Column("codAbrv")]
        [StringLength(20)]
        public string CodAbrv { get; set; }
        [Column("codTax")]
        public short? CodTax { get; set; }
        [Column("codShowBdgt")]
        public byte? CodShowBdgt { get; set; }
        [Column("codLevel")]
        public byte? CodLevel { get; set; }
        [Column("codShowDailyRpt")]
        public byte? CodShowDailyRpt { get; set; }
        [Column("codArabicName")]
        [StringLength(50)]
        public string CodArabicName { get; set; }
        [Column("codAuxiliaryCost")]
        public double? CodAuxiliaryCost { get; set; }
        [Column("codAuxiliarybyCamp")]
        public byte? CodAuxiliarybyCamp { get; set; }
    }
}
