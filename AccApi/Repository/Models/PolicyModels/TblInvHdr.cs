using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInvHdr")]
    public partial class TblInvHdr
    {
        public TblInvHdr()
        {
            TblInvDtls = new HashSet<TblInvDtl>();
        }

        [Key]
        [Column("invSeq")]
        public int InvSeq { get; set; }
        [Key]
        [Column("invType")]
        public byte InvType { get; set; }
        [Column("invSponsor")]
        public int? InvSponsor { get; set; }
        [Column("invProjectCode")]
        [StringLength(200)]
        public string InvProjectCode { get; set; }
        [Column("invCode")]
        [StringLength(20)]
        public string InvCode { get; set; }
        [Column("invDate", TypeName = "datetime")]
        public DateTime? InvDate { get; set; }
        [Column("invDateFrom", TypeName = "datetime")]
        public DateTime? InvDateFrom { get; set; }
        [Column("invDateTo", TypeName = "datetime")]
        public DateTime? InvDateTo { get; set; }
        [Column("invRemark", TypeName = "text")]
        public string InvRemark { get; set; }
        [Column("invDone")]
        public byte? InvDone { get; set; }
        [Column("invDoneDate", TypeName = "datetime")]
        public DateTime? InvDoneDate { get; set; }
        [Column("invTotalCost")]
        public float? InvTotalCost { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public int? InvLabVisaLocation { get; set; }
        [Column("invTax")]
        public double? InvTax { get; set; }
        [Column("invOTHrRate")]
        public double? InvOthrRate { get; set; }
        [Column("invOTWEHolHrRate")]
        public double? InvOtweholHrRate { get; set; }
        [Column("invconfBy")]
        [StringLength(20)]
        public string InvconfBy { get; set; }
        [Column("invConfDate", TypeName = "datetime")]
        public DateTime? InvConfDate { get; set; }
        [Column("invByOccupDesig")]
        public byte? InvByOccupDesig { get; set; }
        [Column("invDtlType")]
        public byte? InvDtlType { get; set; }
        [Column("invDiscount")]
        public double? InvDiscount { get; set; }
        [Column("invApproved")]
        public byte? InvApproved { get; set; }

        [InverseProperty(nameof(TblInvDtl.IndHdr))]
        public virtual ICollection<TblInvDtl> TblInvDtls { get; set; }
    }
}
