using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubcProdBudget")]
    public partial class TblSubcProdBudget
    {
        [Key]
        [StringLength(10)]
        public string DivSubDiv { get; set; }
        public double? Qty { get; set; }
        [StringLength(10)]
        public string Unit { get; set; }
        [Column("hours")]
        public double? Hours { get; set; }
    }
}
