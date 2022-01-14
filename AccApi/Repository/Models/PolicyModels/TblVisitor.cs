using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblVisitor")]
    public partial class TblVisitor
    {
        public TblVisitor()
        {
            TblDistribHdrVisitors = new HashSet<TblDistribHdrVisitor>();
        }

        [Key]
        [Column("labId")]
        [StringLength(8)]
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
        [StringLength(300)]
        public string LabPhoto { get; set; }
        [Column("labHasIDCard")]
        public bool? LabHasIdcard { get; set; }
        [Column("labProject")]
        public int? LabProject { get; set; }
        [Column("labPhoneAllow")]
        public double? LabPhoneAllow { get; set; }
        [Column("labDOB", TypeName = "datetime")]
        public DateTime? LabDob { get; set; }
        [Column("labTransport")]
        public double? LabTransport { get; set; }
        [Column("labHidden")]
        public byte? LabHidden { get; set; }
        [Column("labSponsor")]
        public int? LabSponsor { get; set; }
        [Column("labIdNo")]
        [StringLength(50)]
        public string LabIdNo { get; set; }
        [StringLength(50)]
        public string LabNbPass { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [Column("labPhone")]
        [StringLength(50)]
        public string LabPhone { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("labInActive")]
        public byte? LabInActive { get; set; }
        [Column("labCompany")]
        [StringLength(50)]
        public string LabCompany { get; set; }
        [Column("labType")]
        public byte? LabType { get; set; }
        [Column("labOccupation")]
        [StringLength(50)]
        public string LabOccupation { get; set; }

        [InverseProperty(nameof(TblDistribHdrVisitor.DisLabNavigation))]
        public virtual ICollection<TblDistribHdrVisitor> TblDistribHdrVisitors { get; set; }
    }
}
