using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLabDraft")]
    public partial class TblLabDraft
    {
        [Key]
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labName")]
        [StringLength(200)]
        public string LabName { get; set; }
        [Column("labNameE")]
        [StringLength(200)]
        public string LabNameE { get; set; }
        [Column("labjob")]
        public int? Labjob { get; set; }
        [Column("LabFName")]
        [StringLength(50)]
        public string LabFname { get; set; }
        [Column("LabLName")]
        [StringLength(50)]
        public string LabLname { get; set; }
        [Column("LabFFName")]
        [StringLength(50)]
        public string LabFfname { get; set; }
        [Column("LabMMName")]
        [StringLength(50)]
        public string LabMmname { get; set; }
        [Column("LabFNameA")]
        [StringLength(50)]
        public string LabFnameA { get; set; }
        [Column("LabLNameA")]
        [StringLength(50)]
        public string LabLnameA { get; set; }
        [Column("LabFFNameA")]
        [StringLength(50)]
        public string LabFfnameA { get; set; }
        [Column("LabMMNameA")]
        [StringLength(50)]
        public string LabMmnameA { get; set; }
        [Column("labMother")]
        [StringLength(50)]
        public string LabMother { get; set; }
        [Column("labMotherA")]
        [StringLength(50)]
        public string LabMotherA { get; set; }
        [Column("labHasPhoto")]
        public bool? LabHasPhoto { get; set; }
        [Column("labPhoto")]
        [StringLength(255)]
        public string LabPhoto { get; set; }
        [Column("labWDate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [Column("labLDate", TypeName = "datetime")]
        public DateTime? LabLdate { get; set; }
        [Column("labHasIDCard")]
        public bool? LabHasIdcard { get; set; }
        [Column("labInActive")]
        public byte? LabInActive { get; set; }
        [Column("labHrsDay")]
        public float? LabHrsDay { get; set; }
        [Column("labProject")]
        public int? LabProject { get; set; }
        [Column("labDayFee")]
        public double? LabDayFee { get; set; }
        [Column("labFood")]
        public double? LabFood { get; set; }
        [Column("labFixedMonthly")]
        public double? LabFixedMonthly { get; set; }
        [Column("labHousing")]
        public double? LabHousing { get; set; }
        [Column("labPositionNo")]
        [StringLength(10)]
        public string LabPositionNo { get; set; }
        [Column("labDsg")]
        public int? LabDsg { get; set; }
        [Column("labPhoneAllow")]
        public double? LabPhoneAllow { get; set; }
        [Column("labDOB", TypeName = "datetime")]
        public DateTime? LabDob { get; set; }
        [Column("labTransport")]
        public double? LabTransport { get; set; }
        [Column("labBankAcc")]
        [StringLength(255)]
        public string LabBankAcc { get; set; }
        [Column("labHidden")]
        public byte? LabHidden { get; set; }
        [Column("labExceptionAttendance")]
        public byte? LabExceptionAttendance { get; set; }
        [Column("labOTRate")]
        public double? LabOtrate { get; set; }
        [Column("labWEHRate")]
        public double? LabWehrate { get; set; }
        [Column("labHolHRate")]
        public double? LabHolHrate { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LabNbPass { get; set; }
        [Column("labPhone")]
        [StringLength(50)]
        public string LabPhone { get; set; }
        [Column("labIdNo")]
        [StringLength(50)]
        public string LabIdNo { get; set; }
        [Column("labSponsor")]
        public int? LabSponsor { get; set; }
        public byte? LabExported { get; set; }
        [Column("labExportedDate", TypeName = "datetime")]
        public DateTime? LabExportedDate { get; set; }
        [Column("labLegacyNo")]
        [StringLength(10)]
        public string LabLegacyNo { get; set; }
        [Column("labApproved")]
        public byte? LabApproved { get; set; }
    }
}
