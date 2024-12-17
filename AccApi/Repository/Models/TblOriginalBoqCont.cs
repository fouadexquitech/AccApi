using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblOriginalBOQ_Cont")]
    public partial class TblOriginalBoqCont
    {
        public int Seq { get; set; }
        [Key]
        [StringLength(50)]
        public string Item { get; set; }
        [Key]
        [StringLength(100)]
        public string BillNo { get; set; }
        [StringLength(50)]
        public string Project { get; set; }
        [StringLength(50)]
        public string Div { get; set; }
        [Column("week")]
        public int? Week { get; set; }
        [StringLength(50)]
        public string DryItem { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
        public double? SellQty { get; set; }
        public double? SellUnitRate { get; set; }
        public double? Submitted { get; set; }
        [StringLength(200)]
        public string Sheet { get; set; }
        [Column("ptPrefix")]
        [StringLength(10)]
        public string PtPrefix { get; set; }
        [Column("ptRowNumber")]
        public short? PtRowNumber { get; set; }
        public double? BudUnitRate { get; set; }
        public double? BudQty { get; set; }
        [StringLength(50)]
        public string Ref { get; set; }
        [Column("contractItem")]
        [StringLength(50)]
        public string ContractItem { get; set; }
    }
}
