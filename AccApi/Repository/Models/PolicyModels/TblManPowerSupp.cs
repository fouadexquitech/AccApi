using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblManPowerSupp")]
    [Index(nameof(MpName), Name = "IX_tblManPowerSupp", IsUnique = true)]
    public partial class TblManPowerSupp
    {
        public TblManPowerSupp()
        {
            TblDistribHdrManPowers = new HashSet<TblDistribHdrManPower>();
            TblManpowerSuppSalaries = new HashSet<TblManpowerSuppSalary>();
        }

        [Key]
        [Column("mpID")]
        public int MpId { get; set; }
        [Column("mpName")]
        [StringLength(50)]
        public string MpName { get; set; }
        [Column("mpAbv")]
        [StringLength(5)]
        public string MpAbv { get; set; }
        [Column("mpNote")]
        [StringLength(255)]
        public string MpNote { get; set; }
        [Column("mpCO")]
        [StringLength(4)]
        public string MpCo { get; set; }
        [Column("mpAddress")]
        [StringLength(75)]
        public string MpAddress { get; set; }
        [Column("mpPhone")]
        [StringLength(15)]
        public string MpPhone { get; set; }
        [Column("mpFax")]
        [StringLength(20)]
        public string MpFax { get; set; }
        [Column("mpEmail")]
        [StringLength(100)]
        public string MpEmail { get; set; }
        [StringLength(10)]
        public string InsertBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [StringLength(10)]
        public string LastUpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
        [Column("mpType")]
        public byte? MpType { get; set; }
        [Column("mpMainSponsor")]
        public short? MpMainSponsor { get; set; }
        public byte? PermissionExcept { get; set; }
        [Column("mpWorkType")]
        public short? MpWorkType { get; set; }
        [Column("mpLabFileAutoNo")]
        public bool? MpLabFileAutoNo { get; set; }
        [Column("mpFileMaxRange")]
        public int? MpFileMaxRange { get; set; }
        [Column("mpGOSI")]
        public byte? MpGosi { get; set; }
        [Column("mpInvoiceType")]
        public byte? MpInvoiceType { get; set; }
        [Column("mpLogo")]
        [StringLength(50)]
        public string MpLogo { get; set; }
        [Column("mpIsManPowSup")]
        public bool MpIsManPowSup { get; set; }
        [Column("mpProject")]
        [StringLength(20)]
        public string MpProject { get; set; }
        [Column("mpOTHRate")]
        public double? MpOthrate { get; set; }
        [Column("mpStock")]
        public byte? MpStock { get; set; }
        [Column("mpAllowContraHrs")]
        public byte? MpAllowContraHrs { get; set; }
        [Column("mpMainCompany")]
        [StringLength(50)]
        public string MpMainCompany { get; set; }

        [InverseProperty(nameof(TblDistribHdrManPower.DisLabNavigation))]
        public virtual ICollection<TblDistribHdrManPower> TblDistribHdrManPowers { get; set; }
        [InverseProperty(nameof(TblManpowerSuppSalary.Mph))]
        public virtual ICollection<TblManpowerSuppSalary> TblManpowerSuppSalaries { get; set; }
    }
}
