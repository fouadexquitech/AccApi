using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblUsersZones")]
    public partial class TblUsersZone
    {
        [Key]
        [Column("uzUserID")]
        [StringLength(10)]
        public string UzUserId { get; set; }
        [Key]
        [Column("uzZoneID")]
        [StringLength(50)]
        public string UzZoneId { get; set; }
        [Key]
        [Column("uzProjID")]
        public int UzProjId { get; set; }
    }
}
