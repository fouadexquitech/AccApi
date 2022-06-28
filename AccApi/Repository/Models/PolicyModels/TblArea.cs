using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAreas")]
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
        [StringLength(30)]
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
    }
}
