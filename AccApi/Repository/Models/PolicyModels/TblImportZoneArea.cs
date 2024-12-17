using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblImportZoneArea")]
    public partial class TblImportZoneArea
    {
        [StringLength(50)]
        public string ProjectDef { get; set; }
        [StringLength(50)]
        public string ZoneName { get; set; }
        [StringLength(50)]
        public string AreaName { get; set; }
        [Column("Location_SubArea")]
        [StringLength(50)]
        public string LocationSubArea { get; set; }
        [Column("Sub-Zone")]
        [StringLength(100)]
        public string SubZone { get; set; }
        [StringLength(100)]
        public string Sector { get; set; }
        [Column("projectId")]
        public int? ProjectId { get; set; }
    }
}
