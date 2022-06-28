using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDailyIdleHours")]
    public partial class TblDailyIdleHour
    {
        [Key]
        [Column("idlDate", TypeName = "datetime")]
        public DateTime IdlDate { get; set; }
        [Key]
        [Column("idlType")]
        public int IdlType { get; set; }
        [Key]
        [Column("idlProject")]
        [StringLength(9)]
        public string IdlProject { get; set; }
        [Key]
        [Column("idlForman")]
        public int IdlForman { get; set; }
        [Key]
        [Column("idlArea")]
        public int IdlArea { get; set; }
        [Key]
        [Column("idlZone")]
        public int IdlZone { get; set; }
        [Column("idlTimein", TypeName = "datetime")]
        public DateTime? IdlTimein { get; set; }
        [Column("idlTimeout", TypeName = "datetime")]
        public DateTime? IdlTimeout { get; set; }
        [Column("idlHours")]
        public double? IdlHours { get; set; }
        [Column("idlProjDef")]
        [StringLength(20)]
        public string IdlProjDef { get; set; }
        [Column("idlWBS")]
        [StringLength(35)]
        public string IdlWbs { get; set; }
        [Column("idlNotes")]
        [StringLength(150)]
        public string IdlNotes { get; set; }
        [StringLength(10)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(10)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
