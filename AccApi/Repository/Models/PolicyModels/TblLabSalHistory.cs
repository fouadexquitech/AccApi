using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLabSalHistory")]
    public partial class TblLabSalHistory
    {
        [Column("lshSeq")]
        public int LshSeq { get; set; }
        [Key]
        [Column("lshLabSeq")]
        [StringLength(8)]
        public string LshLabSeq { get; set; }
        [Key]
        [Column("lshDate", TypeName = "datetime")]
        public DateTime LshDate { get; set; }
        [Column("lshDayFeeOld")]
        public double? LshDayFeeOld { get; set; }
        [Column("lshFoodOld")]
        public double? LshFoodOld { get; set; }
        [Column("lshFixedMonthlyOld")]
        public double? LshFixedMonthlyOld { get; set; }
        [Column("lshHousingOld")]
        public double? LshHousingOld { get; set; }
        [Column("lshDayFeeNew")]
        public double? LshDayFeeNew { get; set; }
        [Column("lshFoodNew")]
        public double? LshFoodNew { get; set; }
        [Column("lshFixedMonthlyNew")]
        public double? LshFixedMonthlyNew { get; set; }
        [Column("lshHousingNew")]
        public double? LshHousingNew { get; set; }
        [Column("lshPhoneAllowOld")]
        public double? LshPhoneAllowOld { get; set; }
        [Column("lshPhoneAllowNew")]
        public double? LshPhoneAllowNew { get; set; }
        [Column("lshTransportOld")]
        public double? LshTransportOld { get; set; }
        [Column("lshbPhoneAllowNew")]
        public double? LshbPhoneAllowNew { get; set; }
        [Column("lshLabSponsorOld")]
        public int? LshLabSponsorOld { get; set; }
        [Column("lshTransportNew")]
        public double? LshTransportNew { get; set; }
        [Column("lshlabjobOld")]
        public int? LshlabjobOld { get; set; }
        [Column("lshWEPayTypeOld")]
        public byte? LshWepayTypeOld { get; set; }
        [Column("lshWEPayTypeNew")]
        public byte? LshWepayTypeNew { get; set; }
        [Column("lshExceptionAttendanceold")]
        public byte? LshExceptionAttendanceold { get; set; }
        [Column("lshExceptionAttendanceNew")]
        public byte? LshExceptionAttendanceNew { get; set; }
        [Column("lshOTRateold")]
        public double? LshOtrateold { get; set; }
        [Column("lshOTRateNew")]
        public float? LshOtrateNew { get; set; }
        [Column("lshWEHRateold")]
        public double? LshWehrateold { get; set; }
        [Column("lshWEHRateNew")]
        public double? LshWehrateNew { get; set; }
        [Column("lshHolHRateold")]
        public double? LshHolHrateold { get; set; }
        [Column("lshHolHRateNew")]
        public double? LshHolHrateNew { get; set; }
        [Column("lshHardship")]
        public double? LshHardship { get; set; }
        [Column("lshLabSponsorNew")]
        public int? LshLabSponsorNew { get; set; }
        [Column("lshlabjobNew")]
        public int? LshlabjobNew { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(10)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column("lshVisitVisa")]
        public byte? LshVisitVisa { get; set; }
        [Column("lshlabHrsDay")]
        public double? LshlabHrsDay { get; set; }

        [ForeignKey(nameof(LshLabSeq))]
        [InverseProperty(nameof(TblLab.TblLabSalHistories))]
        public virtual TblLab LshLabSeqNavigation { get; set; }
    }
}
