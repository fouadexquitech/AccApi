using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tmp1")]
    public partial class Tmp1
    {
        [Column("t1")]
        [StringLength(500)]
        public string T1 { get; set; }
        [Column("t11")]
        [StringLength(500)]
        public string T11 { get; set; }
    }
}
