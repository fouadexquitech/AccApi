using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("TblBankLab")]
    public partial class TblBankLab
    {
        [Column("labId")]
        [StringLength(14)]
        public string LabId { get; set; }
        [StringLength(5)]
        public string LabTitle { get; set; }
        [Column("labname")]
        [StringLength(75)]
        public string Labname { get; set; }
        [Column("LabFName")]
        [StringLength(75)]
        public string LabFname { get; set; }
        [Column("LabLName")]
        [StringLength(75)]
        public string LabLname { get; set; }
        [Column("LabFFName")]
        [StringLength(75)]
        public string LabFfname { get; set; }
        [StringLength(200)]
        public string NameP { get; set; }
        [StringLength(1)]
        public string LabSex { get; set; }
        [Column("LabMStatus")]
        [StringLength(1)]
        public string LabMstatus { get; set; }
        [Column("labDOB", TypeName = "datetime")]
        public DateTime? LabDob { get; set; }
        [StringLength(2)]
        public string Nat { get; set; }
        [StringLength(2)]
        public string SecNat { get; set; }
        [StringLength(25)]
        public string LabNbPass { get; set; }
        [Column("labPhone")]
        [StringLength(50)]
        public string LabPhone { get; set; }
        [StringLength(50)]
        public string LabMobile { get; set; }
    }
}
