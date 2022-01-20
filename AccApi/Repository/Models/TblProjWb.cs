using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblProjWBS")]
    public partial class TblProjWb
    {
        [Column("psProjCode")]
        public int PsProjCode { get; set; }
        [Required]
        [Column("psProjWBS")]
        [StringLength(40)]
        public string PsProjWbs { get; set; }
        [Column("psProjDesc")]
        [StringLength(100)]
        public string PsProjDesc { get; set; }
        [Column("psProjMain")]
        public bool? PsProjMain { get; set; }
    }
}
