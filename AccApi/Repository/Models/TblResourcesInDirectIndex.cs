using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesInDirectIndex")]
    public partial class TblResourcesInDirectIndex
    {
        public TblResourcesInDirectIndex()
        {
            TblResourcesInDirects = new HashSet<TblResourcesInDirect>();
        }

        [Key]
        [Column("riiSeq")]
        [StringLength(14)]
        public string RiiSeq { get; set; }
        [Column("riiCtg")]
        public byte RiiCtg { get; set; }
        [Required]
        [Column("riiGrp")]
        [StringLength(14)]
        public string RiiGrp { get; set; }
        [Column("riiTrade")]
        [StringLength(10)]
        public string RiiTrade { get; set; }
        [Column("riiSubDiv")]
        [StringLength(5)]
        public string RiiSubDiv { get; set; }
        [Column("riiDesc")]
        public string RiiDesc { get; set; }
        [Column("riiOrder")]
        public short? RiiOrder { get; set; }
        [Column("riiLtrPerHr")]
        public byte? RiiLtrPerHr { get; set; }
        [Column("riiPartPerHrs")]
        public double? RiiPartPerHrs { get; set; }
        [Column("riiGroup")]
        [StringLength(14)]
        public string RiiGroup { get; set; }
        [Column("riiFieldName")]
        [StringLength(25)]
        public string RiiFieldName { get; set; }
        [Column("riiDiv")]
        [StringLength(2)]
        public string RiiDiv { get; set; }
        [Column("riiSubDivCode")]
        [StringLength(3)]
        public string RiiSubDivCode { get; set; }
        [Column("riiTradeCode")]
        [StringLength(5)]
        public string RiiTradeCode { get; set; }
        [Column("riiSubTrade")]
        [StringLength(3)]
        public string RiiSubTrade { get; set; }
        [Column("riiCode")]
        [StringLength(10)]
        public string RiiCode { get; set; }
        [Column("riisBudget_L")]
        public double? RiisBudgetL { get; set; }
        [Column("riisBudget_M")]
        public double? RiisBudgetM { get; set; }
        [Column("riisBudget_S")]
        public double? RiisBudgetS { get; set; }
        [Column("riisBudget_E")]
        public double? RiisBudgetE { get; set; }
        [Column("riiSort")]
        public int? RiiSort { get; set; }
        [Column("riiProductiveLab")]
        public byte? RiiProductiveLab { get; set; }
        [Column("riisBudget_O")]
        public double? RiisBudgetO { get; set; }
        [Column("riiSeparatItem")]
        public byte? RiiSeparatItem { get; set; }

        [InverseProperty(nameof(TblResourcesInDirect.RinHdrSeqNavigation))]
        public virtual ICollection<TblResourcesInDirect> TblResourcesInDirects { get; set; }
    }
}
