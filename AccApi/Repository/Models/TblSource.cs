using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblSource")]
    public partial class TblSource
    {
        [Column("seq")]
        public int Seq { get; set; }
        [StringLength(50)]
        public string Source { get; set; }
        [StringLength(50)]
        public string ServerName { get; set; }
    }
}
