using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAreas")]
    [Index(nameof(ArProj), nameof(ArName), nameof(ArProjDef), Name = "IX_tblAreas", IsUnique = true)]
    public partial class TblArea
    {
        [Key]
        [Column("arId")]
        public int ArId { get; set; }
        [Column("arProj")]
        public int ArProj { get; set; }
        [Required]
        [Column("arProjDef")]
        [StringLength(30)]
        public string ArProjDef { get; set; }
        [Column("arZone")]
        public int? ArZone { get; set; }
        [Column("arName")]
        [StringLength(50)]
        public string ArName { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("arLevel")]
        [StringLength(50)]
        public string ArLevel { get; set; }
        [Column("arSiteEng")]
        public int? ArSiteEng { get; set; }
        [Column("arPhase")]
        public int? ArPhase { get; set; }
        [Column("arPackage")]
        [StringLength(100)]
        public string ArPackage { get; set; }
        [Column("Sub-Zone")]
        [StringLength(100)]
        public string SubZone { get; set; }
        [StringLength(100)]
        public string Sector { get; set; }
    }
}
