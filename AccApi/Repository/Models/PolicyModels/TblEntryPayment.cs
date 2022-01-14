using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEntryPayment")]
    public partial class TblEntryPayment
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("payType")]
        public bool? PayType { get; set; }
        [Column("payBalanceIssued")]
        public byte? PayBalanceIssued { get; set; }
        [Column("payBalanceAmt")]
        public double? PayBalanceAmt { get; set; }
        [Column("payBalanceDate", TypeName = "datetime")]
        public DateTime? PayBalanceDate { get; set; }
        [Column("payWrkLicenseIssued")]
        public byte? PayWrkLicenseIssued { get; set; }
        [Column("payWrkLicenseAmt")]
        public double? PayWrkLicenseAmt { get; set; }
        [Column("payWrkLicenseDate", TypeName = "datetime")]
        public DateTime? PayWrkLicenseDate { get; set; }
        [Column("payFineAmt")]
        public double? PayFineAmt { get; set; }
        [Column("payNote")]
        [StringLength(200)]
        public string PayNote { get; set; }
        [Column("payAttach")]
        [StringLength(150)]
        public string PayAttach { get; set; }
        [StringLength(20)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [StringLength(20)]
        public string LuserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LdateUpdate { get; set; }
        [StringLength(50)]
        public string EntryNoHdr { get; set; }
        [Column("payWrkLicenseNb")]
        [StringLength(50)]
        public string PayWrkLicenseNb { get; set; }
    }
}
