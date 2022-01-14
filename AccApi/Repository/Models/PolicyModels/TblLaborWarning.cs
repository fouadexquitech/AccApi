using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLaborWarnings")]
    public partial class TblLaborWarning
    {
        [Key]
        [Column("lwSeq")]
        public int LwSeq { get; set; }
        [Key]
        [Column("lwLabId")]
        [StringLength(10)]
        public string LwLabId { get; set; }
        [Column("lwName")]
        [StringLength(200)]
        public string LwName { get; set; }
        [Column("lwType")]
        public byte? LwType { get; set; }
        [Column("lwComments")]
        [StringLength(300)]
        public string LwComments { get; set; }
        [Column("lwPunishDays")]
        public short? LwPunishDays { get; set; }
        [Column("lwDate", TypeName = "datetime")]
        public DateTime? LwDate { get; set; }
        [Column("lwRecommBy")]
        [StringLength(10)]
        public string LwRecommBy { get; set; }
        [Column("lwIssuedBy")]
        [StringLength(10)]
        public string LwIssuedBy { get; set; }
        [Column("lwApprovedBy")]
        [StringLength(10)]
        public string LwApprovedBy { get; set; }
        [Column("lwRecommByName")]
        [StringLength(200)]
        public string LwRecommByName { get; set; }
        [Column("lwIssuedByName")]
        [StringLength(200)]
        public string LwIssuedByName { get; set; }
        [Column("lwApprovedByName")]
        [StringLength(200)]
        public string LwApprovedByName { get; set; }
        [Column("lwType1")]
        public bool? LwType1 { get; set; }
        [Column("lwType2")]
        public bool? LwType2 { get; set; }
        [Column("lwType3")]
        public bool? LwType3 { get; set; }
        [Column("lwType4")]
        public bool? LwType4 { get; set; }
        [Column("lwType5")]
        public bool? LwType5 { get; set; }
        [Column("lwType6")]
        public bool? LwType6 { get; set; }
        [Column("lwType7")]
        public bool? LwType7 { get; set; }
        [Column("lwType8")]
        public bool? LwType8 { get; set; }
        [Column("lwType9")]
        public bool? LwType9 { get; set; }
        [Column("lwType10")]
        public bool? LwType10 { get; set; }
        [Column("lwType11")]
        public bool? LwType11 { get; set; }
        [Column("lwType12")]
        public bool? LwType12 { get; set; }
        [Column("lwRecommDate", TypeName = "datetime")]
        public DateTime? LwRecommDate { get; set; }
        [Column("lwIssuedDate", TypeName = "datetime")]
        public DateTime? LwIssuedDate { get; set; }
        [Column("lwApprovedDate", TypeName = "datetime")]
        public DateTime? LwApprovedDate { get; set; }
        [Column("lwOthers")]
        [StringLength(200)]
        public string LwOthers { get; set; }
        [Column("lwProjectCode")]
        [StringLength(10)]
        public string LwProjectCode { get; set; }
        [Column("lwLabSeq")]
        public int? LwLabSeq { get; set; }
    }
}
