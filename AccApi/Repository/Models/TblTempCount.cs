using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblTempCount")]
    public partial class TblTempCount
    {
        [Column("tSeq")]
        [StringLength(14)]
        public string TSeq { get; set; }
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labName")]
        [StringLength(75)]
        public string LabName { get; set; }
        [Column("labNat")]
        public int? LabNat { get; set; }
        [Column("disstatus")]
        public int? Disstatus { get; set; }
        public short? AllDays { get; set; }
        [Column("we")]
        public bool? We { get; set; }
        public bool? Hol { get; set; }
        [Column("diddate", TypeName = "datetime")]
        public DateTime? Diddate { get; set; }
        public bool? Absent { get; set; }
        [Column("verified")]
        public bool? Verified { get; set; }
        [Column("labFileNo")]
        [StringLength(50)]
        public string LabFileNo { get; set; }
    }
}
