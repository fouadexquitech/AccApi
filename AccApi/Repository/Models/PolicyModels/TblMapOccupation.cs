using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblMapOccupations")]
    public partial class TblMapOccupation
    {
        [Key]
        [Column("mapTSCode")]
        public short MapTscode { get; set; }
        [Key]
        [Column("mapPayCode")]
        public short MapPayCode { get; set; }
        [Key]
        [Column("mapProjID")]
        public int MapProjId { get; set; }
        [Column("mapPayDesc")]
        [StringLength(100)]
        public string MapPayDesc { get; set; }
        [Column("mapPaySort")]
        public short? MapPaySort { get; set; }
        [Column("mapPayGrp")]
        public short? MapPayGrp { get; set; }
        [Column("mapTSDesc")]
        [StringLength(100)]
        public string MapTsdesc { get; set; }
        [Column("mapTSSort")]
        public short? MapTssort { get; set; }
        [Column("mapTSGrp")]
        public short? MapTsgrp { get; set; }
        [Column("mapDirection")]
        public byte? MapDirection { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
    }
}
