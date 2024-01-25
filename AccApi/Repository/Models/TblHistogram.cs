using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblHistograms")]
    public partial class TblHistogram
    {
        [Key]
        [Column("prResCode")]
        [StringLength(14)]
        public string PrResCode { get; set; }
        [Key]
        public short Revision { get; set; }
        public double? CostMonth { get; set; }
        public double? Cost1 { get; set; }
        public double? Cost2 { get; set; }
        public double? Cost3 { get; set; }
        public double? Cost4 { get; set; }
        public double? Cost5 { get; set; }
        public double? Cost6 { get; set; }
        public double? Cost7 { get; set; }
        public double? Cost8 { get; set; }
        public double? Cost9 { get; set; }
        public double? Cost10 { get; set; }
        public double? Cost11 { get; set; }
        public double? Cost12 { get; set; }
        public double? Cost13 { get; set; }
        public double? Cost14 { get; set; }
        public double? Cost15 { get; set; }
        public double? Cost16 { get; set; }
        public double? Cost17 { get; set; }
        public double? Cost18 { get; set; }
        public double? Cost19 { get; set; }
        public double? Cost20 { get; set; }
        public double? Cost21 { get; set; }
        public double? Cost22 { get; set; }
        public double? Cost23 { get; set; }
        public double? Cost24 { get; set; }
        public double? Cost25 { get; set; }
        public double? Cost26 { get; set; }
        public double? Cost27 { get; set; }
        public double? Cost28 { get; set; }
        public double? Cost29 { get; set; }
        public double? Cost30 { get; set; }
        public double? Cost31 { get; set; }
        public double? Cost32 { get; set; }
        public double? Cost33 { get; set; }
        public double? Cost34 { get; set; }
        public double? Cost35 { get; set; }
        public double? Cost36 { get; set; }
        public double? Cost37 { get; set; }
        public double? Cost38 { get; set; }
        public double? Cost39 { get; set; }
        public double? Cost40 { get; set; }
        public double? Cost41 { get; set; }
        public double? Cost42 { get; set; }
        public double? Cost43 { get; set; }
        public double? Cost44 { get; set; }
        public double? Cost45 { get; set; }
        public double? TotalCost { get; set; }
        [StringLength(50)]
        public string SheetDesc { get; set; }
        public double? Qty1 { get; set; }
        public double? Qty2 { get; set; }
        public double? Qty3 { get; set; }
        public double? Qty4 { get; set; }
        public double? Qty5 { get; set; }
        public double? Qty6 { get; set; }
        public double? Qty7 { get; set; }
        public double? Qty8 { get; set; }
        public double? Qty9 { get; set; }
        public double? Qty10 { get; set; }
        public double? Qty11 { get; set; }
        public double? Qty12 { get; set; }
        public double? Qty13 { get; set; }
        public double? Qty14 { get; set; }
        public double? Qty15 { get; set; }
        public double? Qty16 { get; set; }
        public double? Qty17 { get; set; }
        public double? Qty18 { get; set; }
        public double? Qty19 { get; set; }
        public double? Qty20 { get; set; }
        public double? Qty21 { get; set; }
        public double? Qty22 { get; set; }
        public double? Qty23 { get; set; }
        public double? Qty24 { get; set; }
        public double? Qty25 { get; set; }
        public double? Qty26 { get; set; }
        public double? Qty27 { get; set; }
        public double? Qty28 { get; set; }
        public double? Qty29 { get; set; }
        public double? Qty30 { get; set; }
        public double? Qty31 { get; set; }
        public double? Qty32 { get; set; }
        public double? Qty33 { get; set; }
        public double? Qty34 { get; set; }
        public double? Qty35 { get; set; }
        public double? Qty36 { get; set; }
        public double? Qty37 { get; set; }
        public double? Qty38 { get; set; }
        public double? Qty39 { get; set; }
        public double? Qty40 { get; set; }
        public double? Qty41 { get; set; }
        public double? Qty42 { get; set; }
        public double? Qty43 { get; set; }
        public double? Qty44 { get; set; }
        public double? Qty45 { get; set; }
        public double? TotaQty { get; set; }
        public short? RowNumber { get; set; }
        public double? Cost46 { get; set; }
        public double? Qty46 { get; set; }
        public double? Cost47 { get; set; }
        public double? Qty47 { get; set; }
        public double? Cost48 { get; set; }
        public double? Qty48 { get; set; }
        public double? Cost49 { get; set; }
        public double? Qty49 { get; set; }
        public double? Cost50 { get; set; }
        public double? Qty50 { get; set; }
        public double? Cost51 { get; set; }
        public double? Qty51 { get; set; }
    }
}
