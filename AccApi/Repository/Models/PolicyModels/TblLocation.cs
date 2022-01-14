using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLocation")]
    public partial class TblLocation
    {
        [Key]
        [Column("locId")]
        public int LocId { get; set; }
        [Key]
        [Column("locProj")]
        public int LocProj { get; set; }
        [Column("locProjDef")]
        [StringLength(30)]
        public string LocProjDef { get; set; }
        [Column("locBldg")]
        public int? LocBldg { get; set; }
        [Column("locZone")]
        public int? LocZone { get; set; }
        [Column("locArea")]
        public int? LocArea { get; set; }
        [Required]
        [Column("locName")]
        [StringLength(250)]
        public string LocName { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(50)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("locfirst")]
        public bool? Locfirst { get; set; }
    }
}
