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
        [Column("username")]
        [StringLength(100)]
        public string Username { get; set; }
        [Column("subcSubDivQtyCum")]
        public double? SubcSubDivQtyCum { get; set; }
        [Column("subcTradeQtyCum")]
        public double? SubcTradeQtyCum { get; set; }
        [Column("subcHrsW")]
        public double? SubcHrsW { get; set; }
        [Column("subcHrsCum")]
        public double? SubcHrsCum { get; set; }
        public double? CstAccSupervW { get; set; }
        public double? CstAccSupervCum { get; set; }
        [Column("subcUnitPrice")]
        public double? SubcUnitPrice { get; set; }
        [Column("subcSubDivUnitPrice")]
        public double? SubcSubDivUnitPrice { get; set; }
        [Column("subcSubDivQtyW")]
        public double? SubcSubDivQtyW { get; set; }
        [Column("subcTradeQtyW")]
        public double? SubcTradeQtyW { get; set; }
        public double? AccHrsW { get; set; }
        public double? AccHrsCum { get; set; }
        public double? AccCstW { get; set; }
        public double? AccCstCum { get; set; }
        public double? SubcAndAccSupervisionHrsW { get; set; }
        public double? SubcAndAccSupervisionHrsCum { get; set; }
        public double? CstSubcW { get; set; }
        public double? CstSubcCum { get; set; }
        public double? HrsAccSupervW { get; set; }
        public double? HrsAccSupervCum { get; set; }
        public double? AccHourUnitRate { get; set; }
        [Column("subcHrsUnitPrice")]
        public double? SubcHrsUnitPrice { get; set; }
        [Column("subcSubDivCstCum", TypeName = "money")]
        public decimal? SubcSubDivCstCum { get; set; }
    }
}
