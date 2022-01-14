using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblClassSalaryMargin")]
    public partial class TblClassSalaryMargin
    {
        [Key]
        [Column("csmClass")]
        [StringLength(10)]
        public string CsmClass { get; set; }
        [Column("csmMin")]
        public float? CsmMin { get; set; }
        [Column("csmMax")]
        public float? CsmMax { get; set; }
    }
}
