using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblBuildings")]
    public partial class TblBuilding
    {
        [Key]
        [Column("bldgID")]
        public int BldgId { get; set; }
        [Key]
        [Column("bldgProj")]
        public int BldgProj { get; set; }
        [Required]
        [Column("bldgName")]
        [StringLength(250)]
        public string BldgName { get; set; }
        [Column("bldgProjDef")]
        [StringLength(30)]
        public string BldgProjDef { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(50)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("bldgMain")]
        [StringLength(10)]
        public string BldgMain { get; set; }
    }
}
