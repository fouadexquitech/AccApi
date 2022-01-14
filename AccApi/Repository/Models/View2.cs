using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class View2
    {
        [Column("section-o")]
        [StringLength(3)]
        public string SectionO { get; set; }
        public double? Expr1 { get; set; }
    }
}
