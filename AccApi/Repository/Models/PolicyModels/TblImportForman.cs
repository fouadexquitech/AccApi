using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblImportForman")]
    public partial class TblImportForman
    {
        [Key]
        public long Seq { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(10)]
        public string FileNumber { get; set; }
        [StringLength(30)]
        public string Type { get; set; }
        [StringLength(30)]
        public string ProjectDef { get; set; }
        [Column("Site Eng")]
        [StringLength(100)]
        public string SiteEng { get; set; }
        [Column("Section Eng")]
        [StringLength(100)]
        public string SectionEng { get; set; }
        [Column("CM")]
        [StringLength(100)]
        public string Cm { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [StringLength(100)]
        public string FileName { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [Column("General Foreman")]
        [StringLength(100)]
        public string GeneralForeman { get; set; }
        [Column("ProjectID")]
        public int? ProjectId { get; set; }
    }
}
