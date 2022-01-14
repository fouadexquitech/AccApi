using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblImportGOSI")]
    public partial class TblImportGosi
    {
        [Column("GOSI")]
        [StringLength(50)]
        public string Gosi { get; set; }
        [Column("GOSISponsor")]
        [StringLength(50)]
        public string Gosisponsor { get; set; }
        public double? Salary { get; set; }
        [Column("PassportID")]
        [StringLength(50)]
        public string PassportId { get; set; }
        [StringLength(50)]
        public string IkamaNo { get; set; }
        [StringLength(50)]
        public string HafizaNo { get; set; }
        [Column("NationalID")]
        [StringLength(50)]
        public string NationalId { get; set; }
        [StringLength(50)]
        public string HiringDate { get; set; }
        [StringLength(50)]
        public string BirthDate { get; set; }
        [StringLength(255)]
        public string Natinality { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Sponsor { get; set; }
        public bool? Staff { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        [Column("SponsorID")]
        public int? SponsorId { get; set; }
        public byte? Status { get; set; }
    }
}
