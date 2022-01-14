using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblPrelimsGroup")]
    public partial class TblPrelimsGroup
    {
        [Key]
        [Column("prgCode")]
        public int PrgCode { get; set; }
        [Column("prgDescription")]
        [StringLength(150)]
        public string PrgDescription { get; set; }
    }
}
