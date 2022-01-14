using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("area")]
    public partial class Area
    {
        [Column("areaid")]
        public int Areaid { get; set; }
        [Column("areaname")]
        [StringLength(255)]
        public string Areaname { get; set; }
        [Column("kadaid")]
        public int? Kadaid { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
    }
}
