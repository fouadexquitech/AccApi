using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    public partial class LinkZone
    {
        [Key]
        [Column("OldZoneID")]
        [StringLength(50)]
        public string OldZoneId { get; set; }
        public int? OldZoneProj { get; set; }
        [Key]
        [Column("NewZoneID")]
        public int NewZoneId { get; set; }
    }
}
