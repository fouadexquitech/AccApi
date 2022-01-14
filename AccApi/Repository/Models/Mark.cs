using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("mark")]
    public partial class Mark
    {
        [StringLength(50)]
        public string Area { get; set; }
        [StringLength(50)]
        public string Scope { get; set; }
        [Column("Project-o")]
        [StringLength(50)]
        public string ProjectO { get; set; }
        [StringLength(255)]
        public string Expr1 { get; set; }
        public double? Discount1 { get; set; }
        public double? Discount2 { get; set; }
        public double? Discount3 { get; set; }
        public double? Discount4 { get; set; }
    }
}
