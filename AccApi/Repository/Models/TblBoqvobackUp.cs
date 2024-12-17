using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBOQVOBackUp")]
    public partial class TblBoqvobackUp
    {
        [Key]
        [Column("bbuDate", TypeName = "datetime")]
        public DateTime BbuDate { get; set; }
        [Column("bbuActive")]
        public byte? BbuActive { get; set; }
        [Column("bbuProj")]
        [StringLength(10)]
        public string BbuProj { get; set; }
    }
}
