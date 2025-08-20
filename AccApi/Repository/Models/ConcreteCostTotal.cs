using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("ConcreteCostTotal")]
    public partial class ConcreteCostTotal
    {
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
        [Column(TypeName = "money")]
        public decimal? SumOfQty { get; set; }
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
        [Column("targetHr/m3")]
        public double? TargetHrM3 { get; set; }
        [Column("targetCost/m3")]
        public double? TargetCostM3 { get; set; }
        [Column("TargetHr/m3_Acc")]
        public double? TargetHrM3Acc { get; set; }
        [Column("TargetHr/m3_Sub")]
        public double? TargetHrM3Sub { get; set; }
        [Column("TargetCost/m3_Acc")]
        public double? TargetCostM3Acc { get; set; }
        [Column("TargetCost/m3_Sub")]
        public double? TargetCostM3Sub { get; set; }
        [Column("Prod_Acc")]
        public double? ProdAcc { get; set; }
        public double? Prod { get; set; }
        [Column("Prod_Sub")]
        public double? ProdSub { get; set; }
        public double? Qty { get; set; }
        public double? AccQty { get; set; }
        [Column("UPrice_Acc")]
        public double? UpriceAcc { get; set; }
        [Column("UPrice_Sub")]
        public double? UpriceSub { get; set; }
        [Column("UPrice")]
        public double? Uprice { get; set; }
    }
}
