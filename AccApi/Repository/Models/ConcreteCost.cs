using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("ConcreteCost")]
    public partial class ConcreteCost
    {
        [StringLength(30)]
        public string Area { get; set; }
        [StringLength(30)]
        public string DivSubDiv { get; set; }
        [StringLength(50)]
        public string SubDiv { get; set; }
        [Column(TypeName = "money")]
        public decimal? SumOfHrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? SumOfCost { get; set; }
        [Column(TypeName = "money")]
        public decimal? SubQty { get; set; }
        [Column(TypeName = "money")]
        public decimal? SubCost { get; set; }
        [Column("AMH", TypeName = "money")]
        public decimal? Amh { get; set; }
        [Column(TypeName = "money")]
        public decimal? SubHrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? Hrs { get; set; }
        [Column(TypeName = "money")]
        public decimal? Cost { get; set; }
    }
}
