using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblMissingCC")]
    public partial class TblMissingCc
    {
        [Column("mccCC")]
        [StringLength(255)]
        public string MccCc { get; set; }
        [Column("mccDesc")]
        [StringLength(255)]
        public string MccDesc { get; set; }
        [Column("mccAmount")]
        public double? MccAmount { get; set; }
    }
}
