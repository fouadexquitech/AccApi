using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("TblBankLabNew")]
    public partial class TblBankLabNew
    {
        [Column("ID")]
        [StringLength(14)]
        public string Id { get; set; }
        [StringLength(4)]
        public string Cur { get; set; }
        [StringLength(4)]
        public string Title { get; set; }
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
        [Column("codDescE")]
        [StringLength(100)]
        public string CodDescE { get; set; }
        [StringLength(1)]
        public string Gender { get; set; }
        [Column("DOB")]
        [StringLength(10)]
        public string Dob { get; set; }
        [Column("labPhone")]
        [StringLength(50)]
        public string LabPhone { get; set; }
        [StringLength(50)]
        public string LabMobile { get; set; }
        [Column("ladEmail")]
        [StringLength(50)]
        public string LadEmail { get; set; }
        [StringLength(60)]
        public string Bldg { get; set; }
        [Column("ladStr")]
        [StringLength(50)]
        public string LadStr { get; set; }
        [Column("areaname")]
        [StringLength(255)]
        public string Areaname { get; set; }
        [Column("ladRegion")]
        [StringLength(50)]
        public string LadRegion { get; set; }
        [Column("ladPostBox")]
        [StringLength(20)]
        public string LadPostBox { get; set; }
        [StringLength(20)]
        public string Country { get; set; }
        [StringLength(150)]
        public string Filler { get; set; }
    }
}
