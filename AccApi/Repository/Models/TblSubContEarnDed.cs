using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubContEarnDed")]
    public partial class TblSubContEarnDed
    {
        [Key]
        [Column("sdSeq")]
        [StringLength(14)]
        public string SdSeq { get; set; }
        [Column("sdProject")]
        [StringLength(10)]
        public string SdProject { get; set; }
        [Column("sdSubC")]
        [StringLength(12)]
        public string SdSubC { get; set; }
        [Column("sdWeek")]
        public int? SdWeek { get; set; }
        [Column("sdTrade")]
        [StringLength(10)]
        public string SdTrade { get; set; }
        [Column("sdType")]
        public byte? SdType { get; set; }
        [Column("sdPer")]
        public float? SdPer { get; set; }
        [Column("sdValue")]
        public float? SdValue { get; set; }
        [Column("sdBOQ")]
        [StringLength(20)]
        public string SdBoq { get; set; }
        [Column("sdNote")]
        [StringLength(255)]
        public string SdNote { get; set; }
        [Column("sdDiv")]
        [StringLength(2)]
        public string SdDiv { get; set; }
        [Column("sdSubDiv")]
        [StringLength(3)]
        public string SdSubDiv { get; set; }
        [Column("sdTradeCode")]
        [StringLength(5)]
        public string SdTradeCode { get; set; }
        [Column("sdSubTrade")]
        [StringLength(5)]
        public string SdSubTrade { get; set; }
        [Column("sdExecVariation")]
        public float? SdExecVariation { get; set; }
        [Column("sdPC")]
        [StringLength(10)]
        public string SdPc { get; set; }
        [Column("sdAccPC")]
        [StringLength(10)]
        public string SdAccPc { get; set; }
        [Column("sdMonthEnd", TypeName = "datetime")]
        public DateTime? SdMonthEnd { get; set; }
        [Column("sdDivision")]
        [StringLength(50)]
        public string SdDivision { get; set; }
        [Column("sdSkip")]
        public byte SdSkip { get; set; }
    }
}
