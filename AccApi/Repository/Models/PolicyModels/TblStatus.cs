using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblStatus")]
    public partial class TblStatus
    {
        [Key]
        [Column("asSeq")]
        public short AsSeq { get; set; }
        [Column("asDesc")]
        [StringLength(25)]
        public string AsDesc { get; set; }
        [Column("ashours")]
        public float? Ashours { get; set; }
        [Column("asmaxdays")]
        public short? Asmaxdays { get; set; }
        [Column("asHasCost")]
        public byte? AsHasCost { get; set; }
        [Column("asAbv")]
        [StringLength(5)]
        public string AsAbv { get; set; }
        [Column("asSort")]
        public int? AsSort { get; set; }
        [Column("asTKUse")]
        public int? AsTkuse { get; set; }
        [Column("asGroup")]
        public int? AsGroup { get; set; }
    }
}
