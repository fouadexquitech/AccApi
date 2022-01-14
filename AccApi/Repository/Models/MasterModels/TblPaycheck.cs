using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblPaycheck")]
    public partial class TblPaycheck
    {
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [Column("scpYear")]
        [StringLength(6)]
        public string ScpYear { get; set; }
        [Key]
        [Column("scpMonth")]
        [StringLength(5)]
        public string ScpMonth { get; set; }
        [Key]
        [Column("scpName")]
        [StringLength(200)]
        public string ScpName { get; set; }
        [Column("scpSeq")]
        public int? ScpSeq { get; set; }
        [Column("scpSheetType")]
        [StringLength(50)]
        public string ScpSheetType { get; set; }
        [Column("scpJob")]
        [StringLength(200)]
        public string ScpJob { get; set; }
        [Column("scpCeiling")]
        public int? ScpCeiling { get; set; }
        [Column("scpTotPresentDay")]
        public int? ScpTotPresentDay { get; set; }
        [Column("scpTotAbsentDay")]
        public int? ScpTotAbsentDay { get; set; }
        [Column("scpTotPresentHrs")]
        public int? ScpTotPresentHrs { get; set; }
        [Column("scpRequiredHrs")]
        public int? ScpRequiredHrs { get; set; }
        [Column("scpNetSalary")]
        public int? ScpNetSalary { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
    }
}
