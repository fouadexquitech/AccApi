using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubcontractor")]
    public partial class TblSubcontractor
    {
        [Key]
        [Column("ID")]
        [StringLength(12)]
        public string Id { get; set; }
        [StringLength(50)]
        public string SubName { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        [StringLength(12)]
        public string Forman { get; set; }
    }
}
