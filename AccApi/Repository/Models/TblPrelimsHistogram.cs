using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblPrelimsHistogram")]
    public partial class TblPrelimsHistogram
    {
        [Key]
        [Column("prCateg")]
        public int PrCateg { get; set; }
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
    }
}
