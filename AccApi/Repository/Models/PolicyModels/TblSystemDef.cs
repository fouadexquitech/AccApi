using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSystemDef")]
    public partial class TblSystemDef
    {
        [Key]
        [Column("sdSeq")]
        public int SdSeq { get; set; }
        [Column("sdDataLife")]
        public short? SdDataLife { get; set; }
        [Column("sdTimeRoundIn")]
        public short? SdTimeRoundIn { get; set; }
        [Column("sdTimeRoundOut")]
        public short? SdTimeRoundOut { get; set; }
        [Column("sdMaxOTHrs")]
        public double? SdMaxOthrs { get; set; }
        [Column("sdApplyFridayFrac")]
        public byte? SdApplyFridayFrac { get; set; }
        [Column("sdApplyWEDays")]
        public byte? SdApplyWedays { get; set; }
        [Column("sdCeiling")]
        public double? SdCeiling { get; set; }
        [Column("sdProbationPeriod")]
        public byte? SdProbationPeriod { get; set; }
        [Column("sdTaxRate")]
        public double? SdTaxRate { get; set; }
        [Column("sdPhotoLocation")]
        [StringLength(200)]
        public string SdPhotoLocation { get; set; }
        [Column("sdCompanyTitle")]
        [StringLength(100)]
        public string SdCompanyTitle { get; set; }
        [Column("sdProjectLogo")]
        [StringLength(200)]
        public string SdProjectLogo { get; set; }
        [Column("sdLastNoLabID")]
        public int? SdLastNoLabId { get; set; }
        [Column("sdtaxLunchTime")]
        public float? SdtaxLunchTime { get; set; }
        [Column("sdDailyMonthly")]
        public byte? SdDailyMonthly { get; set; }
        [Column("sdFixedTax")]
        public byte? SdFixedTax { get; set; }
        [Column("sdApplyTax")]
        public byte? SdApplyTax { get; set; }
        [Column("sdApplyExchange")]
        public byte? SdApplyExchange { get; set; }
        [Column("sdApplyExcepOt")]
        public byte? SdApplyExcepOt { get; set; }
        [Column("sdUnlockPass")]
        [StringLength(30)]
        public string SdUnlockPass { get; set; }
        [Column("sdCurrency")]
        [StringLength(30)]
        public string SdCurrency { get; set; }
        [Column("sdPayData")]
        [StringLength(50)]
        public string SdPayData { get; set; }
        [Column("sdMultipleAttendance")]
        public byte? SdMultipleAttendance { get; set; }
        [Column("sdCompanyName")]
        [StringLength(100)]
        public string SdCompanyName { get; set; }
        [Column("sdLabSkill_Force")]
        public byte? SdLabSkillForce { get; set; }
        [Column("sdPayrollCurrency")]
        [StringLength(5)]
        public string SdPayrollCurrency { get; set; }
        [Column("sdPayrollMonAllow")]
        public byte? SdPayrollMonAllow { get; set; }
        [Column("sdbankName")]
        [StringLength(50)]
        public string SdbankName { get; set; }
        [Column("sdBankBranch")]
        public int? SdBankBranch { get; set; }
        [Column("sdAllowIdleHrs")]
        public byte? SdAllowIdleHrs { get; set; }
        [Column("sdAllowSmrHrs")]
        public byte? SdAllowSmrHrs { get; set; }
    }
}
