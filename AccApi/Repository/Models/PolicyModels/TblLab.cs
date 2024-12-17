using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLab")]
    public partial class TblLab
    {
        public TblLab()
        {
            TblDistribHdrs = new HashSet<TblDistribHdr>();
            TblMonthlyAddDeds = new HashSet<TblMonthlyAddDed>();
        }

        [Key]
        [Column("labId")]
        [StringLength(8)]
        public string LabId { get; set; }
        [Column("labLegacyNo")]
        [StringLength(20)]
        public string LabLegacyNo { get; set; }
        [Column("labName")]
        [StringLength(250)]
        public string LabName { get; set; }
        [Column("labNameE")]
        [StringLength(200)]
        public string LabNameE { get; set; }
        [Column("labjob")]
        public int? Labjob { get; set; }
        [Column("LabFName")]
        [StringLength(75)]
        public string LabFname { get; set; }
        [Column("LabLName")]
        [StringLength(75)]
        public string LabLname { get; set; }
        [Column("LabFFName")]
        [StringLength(75)]
        public string LabFfname { get; set; }
        [Column("LabMMName")]
        [StringLength(75)]
        public string LabMmname { get; set; }
        [Column("LabFNameA")]
        [StringLength(75)]
        public string LabFnameA { get; set; }
        [Column("LabLNameA")]
        [StringLength(75)]
        public string LabLnameA { get; set; }
        [Column("LabFFNameA")]
        [StringLength(75)]
        public string LabFfnameA { get; set; }
        [Column("LabMMNameA")]
        [StringLength(75)]
        public string LabMmnameA { get; set; }
        [Column("labMother")]
        [StringLength(75)]
        public string LabMother { get; set; }
        [Column("labMotherA")]
        [StringLength(75)]
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
        [Column("labHidden")]
        public byte? LabHidden { get; set; }
        [Column("labSponsor")]
        public int? LabSponsor { get; set; }
        [Column("labIdNo")]
        [StringLength(50)]
        public string LabIdNo { get; set; }
        [StringLength(50)]
        public string LabNbPass { get; set; }
        [Column("labprojectCode")]
        [StringLength(20)]
        public string LabprojectCode { get; set; }
        [Column("labSkilled")]
        public byte? LabSkilled { get; set; }
        [Column("labSemiEmp")]
        public bool? LabSemiEmp { get; set; }
        [Column("labPhone")]
        [StringLength(50)]
        public string LabPhone { get; set; }
        [Column("LabFAccNew")]
        [StringLength(8)]
        public string LabFaccNew { get; set; }
        [Column("labNationalNo")]
        [StringLength(50)]
        public string LabNationalNo { get; set; }
        [Column("labType")]
        public byte? LabType { get; set; }
        [Column("labEntryNo")]
        [StringLength(50)]
        public string LabEntryNo { get; set; }
        [Column("labWrkRef")]
        [StringLength(10)]
        public string LabWrkRef { get; set; }
        [Column("labEndRef")]
        [StringLength(3)]
        public string LabEndRef { get; set; }
        [Column("labTrskRef")]
        [StringLength(3)]
        public string LabTrskRef { get; set; }
        [Column("LabBB")]
        [StringLength(5)]
        public string LabBb { get; set; }
        [Column("labbank")]
        [StringLength(50)]
        public string Labbank { get; set; }
        [Column("labWEPayType")]
        public byte? LabWepayType { get; set; }
        [Column("labFileNo")]
        [StringLength(15)]
        public string LabFileNo { get; set; }
        [Column("labNew")]
        public byte? LabNew { get; set; }
        [Column("labSalType")]
        public byte? LabSalType { get; set; }
        [Column("labCardExpire", TypeName = "datetime")]
        public DateTime? LabCardExpire { get; set; }
        [Column("labSafetyInd")]
        public byte? LabSafetyInd { get; set; }
        [Column("labClassSalary")]
        [StringLength(5)]
        public string LabClassSalary { get; set; }
        [Column("labAccomlocation")]
        public int? LabAccomlocation { get; set; }
        [Column("labAccomRoom")]
        public int? LabAccomRoom { get; set; }
        [Column("labAccomDate", TypeName = "datetime")]
        public DateTime? LabAccomDate { get; set; }
        [Column("labPhoneCountry")]
        [StringLength(50)]
        public string LabPhoneCountry { get; set; }
        [Column("labReportToPolice")]
        public bool? LabReportToPolice { get; set; }
        public int? Labsex { get; set; }
        public int? LabTitle { get; set; }
        [Column("LabMStatus")]
        public int? LabMstatus { get; set; }
        [Column("LabBCard")]
        public bool? LabBcard { get; set; }
        [Column("labShowSalRpt")]
        public bool? LabShowSalRpt { get; set; }
        public bool? LabWithoutTax { get; set; }
        [Column("labUnrealName")]
        [StringLength(200)]
        public string LabUnrealName { get; set; }
        [Column("labIdExpireDate", TypeName = "datetime")]
        public DateTime? LabIdExpireDate { get; set; }
        [Column("labWorkPermitIssueDate", TypeName = "datetime")]
        public DateTime? LabWorkPermitIssueDate { get; set; }
        [Column("labWorkPermitExpiryDate", TypeName = "datetime")]
        public DateTime? LabWorkPermitExpiryDate { get; set; }
        [Column("labVisaIssueDate", TypeName = "datetime")]
        public DateTime? LabVisaIssueDate { get; set; }
        [Column("labVisaExpiryDate", TypeName = "datetime")]
        public DateTime? LabVisaExpiryDate { get; set; }
        [Column("labHasTransportation")]
        public bool? LabHasTransportation { get; set; }
        [Column("labInsuranceIssueDate", TypeName = "datetime")]
        public DateTime? LabInsuranceIssueDate { get; set; }
        [Column("labInsuranceExpiryDate", TypeName = "datetime")]
        public DateTime? LabInsuranceExpiryDate { get; set; }
        [Column("labHasAccommodation")]
        public bool? LabHasAccommodation { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("labInsurancePin")]
        [StringLength(50)]
        public string LabInsurancePin { get; set; }
        [Column("labGOSI")]
        [StringLength(50)]
        public string LabGosi { get; set; }
        [StringLength(10)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LabPassExpireDate { get; set; }
        public bool? LabManualEntryPerm { get; set; }
        [Column("labNssfNb")]
        [StringLength(15)]
        public string LabNssfNb { get; set; }
        [Column("labNotes")]
        [StringLength(255)]
        public string LabNotes { get; set; }
        [Column("labBirthPlace")]
        public int? LabBirthPlace { get; set; }
        [Column("labMuslim")]
        public byte? LabMuslim { get; set; }
        public int? LabVisaLocation { get; set; }
        public int? LabGovernorate { get; set; }
        [Column("labDepartment")]
        public int? LabDepartment { get; set; }
        [Column("labWithTS")]
        public byte? LabWithTs { get; set; }
        [Column("labTeam")]
        public int? LabTeam { get; set; }
        [Column("labNSSFRegDate", TypeName = "datetime")]
        public DateTime? LabNssfregDate { get; set; }
        [Column("labResidPlace")]
        [StringLength(100)]
        public string LabResidPlace { get; set; }
        [Column("labOTHoursPermit")]
        public bool? LabOthoursPermit { get; set; }
        [Column("labMealType")]
        public int? LabMealType { get; set; }

        [InverseProperty(nameof(TblDistribHdr.DisLabNavigation))]
        public virtual ICollection<TblDistribHdr> TblDistribHdrs { get; set; }
        [InverseProperty(nameof(TblMonthlyAddDed.MadLab))]
        public virtual ICollection<TblMonthlyAddDed> TblMonthlyAddDeds { get; set; }
    }
}
