using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproLab")]
    [Index(nameof(InsertedDate), Name = "IX_tblEproLab")]
    public partial class TblEproLab
    {
        [Column("labEproMasterId", TypeName = "numeric(9, 0)")]
        public decimal? LabEproMasterId { get; set; }
        [Key]
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labWDate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [StringLength(70)]
        public string EproPosition { get; set; }
        [StringLength(100)]
        public string EproPositionDesc { get; set; }
        [Column("labjob")]
        public int? Labjob { get; set; }
        [Column("labDsg")]
        public int? LabDsg { get; set; }
        [Column("EproWORK_PLACE")]
        [StringLength(40)]
        public string EproWorkPlace { get; set; }
        [Column("EproWORK_PlaceDesc")]
        [StringLength(100)]
        public string EproWorkPlaceDesc { get; set; }
        [Column("labprojectCode")]
        [StringLength(500)]
        public string LabprojectCode { get; set; }
        [Column("LabLName")]
        [StringLength(75)]
        public string LabLname { get; set; }
        [Column("LabFName")]
        [StringLength(75)]
        public string LabFname { get; set; }
        [Column("labName")]
        [StringLength(250)]
        public string LabName { get; set; }
        [Column("labNameE")]
        [StringLength(200)]
        public string LabNameE { get; set; }
        [Column("LabFFName")]
        [StringLength(75)]
        public string LabFfname { get; set; }
        [Column("LabFNameA")]
        [StringLength(75)]
        public string LabFnameA { get; set; }
        [Column("LabLNameA")]
        [StringLength(75)]
        public string LabLnameA { get; set; }
        [Column("LabFFNameA")]
        [StringLength(75)]
        public string LabFfnameA { get; set; }
        [Column("labMother")]
        [StringLength(75)]
        public string LabMother { get; set; }
        [Column("labMotherA")]
        [StringLength(75)]
        public string LabMotherA { get; set; }
        [StringLength(100)]
        public string EproVisaLocation { get; set; }
        [Column("labDOB", TypeName = "datetime")]
        public DateTime? LabDob { get; set; }
        [StringLength(100)]
        public string EproCtryOfBirth { get; set; }
        [Column("TSCtryOfBirthID")]
        public int? TsctryOfBirthId { get; set; }
        [StringLength(100)]
        public string EproNatio { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [Column("EproMSTATUS")]
        [StringLength(100)]
        public string EproMstatus { get; set; }
        [Column("LabMStatus")]
        public int? LabMstatus { get; set; }
        [Column("EproRELIGION")]
        [StringLength(100)]
        public string EproReligion { get; set; }
        [Column("EproCONT_TYPE")]
        [StringLength(50)]
        public string EproContType { get; set; }
        [Column("EproCONT_EXP", TypeName = "datetime")]
        public DateTime? EproContExp { get; set; }
        [StringLength(100)]
        public string LabNbPass { get; set; }
        [Column("EproPASSP_ISU", TypeName = "datetime")]
        public DateTime? EproPasspIsu { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LabPassExpireDate { get; set; }
        [Column("EproPASSP_PLS")]
        [StringLength(100)]
        public string EproPasspPls { get; set; }
        [Column("EproPASSP_NAT")]
        [StringLength(70)]
        public string EproPasspNat { get; set; }
        [StringLength(100)]
        public string EproCompany { get; set; }
        [StringLength(100)]
        public string EproCompanyDesc { get; set; }
        [StringLength(50)]
        public string EproCompanyCode { get; set; }
        [Column("labSponsor")]
        public int? LabSponsor { get; set; }
        [StringLength(50)]
        public string TimeAdministrator { get; set; }
        [Column("labDayFee")]
        public double? LabDayFee { get; set; }
        [Column("labFood")]
        public double? LabFood { get; set; }
        [Column("labHousing")]
        public double? LabHousing { get; set; }
        [Column("labFixedMonthly")]
        public double? LabFixedMonthly { get; set; }
        [Column("labTransport")]
        public double? LabTransport { get; set; }
        [Column("labPhoneAllow")]
        public double? LabPhoneAllow { get; set; }
        [Column("labIdNo")]
        [StringLength(50)]
        public string LabIdNo { get; set; }
        [Column("labStaff")]
        [StringLength(50)]
        public string LabStaff { get; set; }
        public byte? IgnoreExport { get; set; }
        [Column("labIdExpireDate", TypeName = "datetime")]
        public DateTime? LabIdExpireDate { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column("labNotes")]
        [StringLength(255)]
        public string LabNotes { get; set; }
        [Column("TSExported")]
        public byte? Tsexported { get; set; }
        [Column("TSExportedDate", TypeName = "date")]
        public DateTime? TsexportedDate { get; set; }
        [Column("SAPExported")]
        public byte? Sapexported { get; set; }
        [Column("SAPExportedDate", TypeName = "date")]
        public DateTime? SapexportedDate { get; set; }
        [Column("SEX")]
        [StringLength(10)]
        public string Sex { get; set; }
    }
}
