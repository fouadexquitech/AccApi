using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAccomRoom")]
    public partial class TblAccomRoom
    {
        [Key]
        [Column("rmId")]
        public int RmId { get; set; }
        [Key]
        [Column("rmCampSeq")]
        public int RmCampSeq { get; set; }
        [Required]
        [Column("rmAbv")]
        [StringLength(10)]
        public string RmAbv { get; set; }
        [Column("rmFloorId")]
        [StringLength(10)]
        public string RmFloorId { get; set; }
        [Column("rmDesc")]
        [StringLength(50)]
        public string RmDesc { get; set; }
        [Column("rmWidth")]
        public double? RmWidth { get; set; }
        [Column("rmHeight")]
        public double? RmHeight { get; set; }
        [Column("rmCapacity")]
        public int? RmCapacity { get; set; }
    }
}
