using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblZones")]
    public partial class TblZone
    {
        [Key]
        [Column("zonID")]
        public int ZonId { get; set; }
        [Key]
        [Column("zonProj")]
        public int ZonProj { get; set; }
        [Column("zonName")]
        [StringLength(50)]
        public string ZonName { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("arSel")]
        public short? ArSel { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("zonPhase")]
        public int? ZonPhase { get; set; }
        [Column("zonForman")]
        [StringLength(12)]
        public string ZonForman { get; set; }
        [Column("zonSubContartor")]
        public bool? ZonSubContartor { get; set; }
        [Column("zonConManager")]
        public int? ZonConManager { get; set; }
    }
}
