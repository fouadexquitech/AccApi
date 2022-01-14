using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAssignFormanEng")]
    public partial class TblAssignFormanEng
    {
        [Column("feSeq")]
        public int FeSeq { get; set; }
        [Key]
        [Column("feForman")]
        public int FeForman { get; set; }
        [Key]
        [Column("feFromDate", TypeName = "datetime")]
        public DateTime FeFromDate { get; set; }
        [Column("feCM")]
        public int? FeCm { get; set; }
        [Column("feSectionEng")]
        public int? FeSectionEng { get; set; }
        [Column("feSiteEng")]
        public int? FeSiteEng { get; set; }
        [StringLength(15)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [StringLength(15)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("feArea")]
        public int? FeArea { get; set; }
        [Column("feWBS")]
        [StringLength(30)]
        public string FeWbs { get; set; }
        [Column("feGeneralForman")]
        public int? FeGeneralForman { get; set; }
        [Column("feProject")]
        public int? FeProject { get; set; }
    }
}
