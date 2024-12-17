using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblVOHdr")]
    public partial class TblVohdr
    {
        public TblVohdr()
        {
            TblVodtls = new HashSet<TblVodtl>();
        }

        [Key]
        [Column("voSeq")]
        public int VoSeq { get; set; }
        [Required]
        [StringLength(50)]
        public string Ref { get; set; }
        [StringLength(2000)]
        public string Description { get; set; }
        public byte? TimeImpact { get; set; }
        [Column("CivilMEP")]
        public int? CivilMep { get; set; }
        [StringLength(50)]
        public string RvAttributedTo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RvDate { get; set; }
        [StringLength(500)]
        public string RvReference { get; set; }
        [StringLength(500)]
        public string ContRef { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ContDate { get; set; }
        [StringLength(50)]
        public string ActionBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActionTargetDate { get; set; }
        [StringLength(1000)]
        public string ActionFurtherRef { get; set; }
        [StringLength(500)]
        public string ContSubmRef { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ContSubmDate { get; set; }
        [Column(TypeName = "money")]
        public decimal? ContSubmAmt { get; set; }
        [StringLength(1000)]
        public string ContSubmRemark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [StringLength(50)]
        public string LastUserUpdate { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }

        [InverseProperty(nameof(TblVodtl.SeqHdrNavigation))]
        public virtual ICollection<TblVodtl> TblVodtls { get; set; }
    }
}
