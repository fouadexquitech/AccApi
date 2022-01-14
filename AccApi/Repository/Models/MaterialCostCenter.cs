using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("MaterialCostCenter")]
    public partial class MaterialCostCenter
    {
        [Key]
        [Column("mcProj")]
        [StringLength(10)]
        public string McProj { get; set; }
        [Key]
        [Column("mcWeek")]
        public int McWeek { get; set; }
        [Key]
        [Column("mcCC")]
        [StringLength(15)]
        public string McCc { get; set; }
        [Key]
        [Column("mcArea")]
        [StringLength(12)]
        public string McArea { get; set; }
        [Column("mcDiv")]
        [StringLength(2)]
        public string McDiv { get; set; }
        [Column("mcSubDiv")]
        [StringLength(3)]
        public string McSubDiv { get; set; }
        [Column("mcTrade")]
        [StringLength(5)]
        public string McTrade { get; set; }
        [Column("mcSubTrade")]
        [StringLength(3)]
        public string McSubTrade { get; set; }
        [Column("mcTotalCost")]
        public double? McTotalCost { get; set; }
    }
}
