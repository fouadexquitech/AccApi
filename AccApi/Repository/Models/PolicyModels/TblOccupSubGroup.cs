using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblOccupSubGroup")]
    public partial class TblOccupSubGroup
    {
        [Key]
        [Column("sgGrpSeq")]
        public int SgGrpSeq { get; set; }
        [Key]
        [Column("sgSeq")]
        public int SgSeq { get; set; }
        [Column("sgName")]
        [StringLength(50)]
        public string SgName { get; set; }
        [Column("sgSort")]
        public int? SgSort { get; set; }
    }
}
