using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("TmpDivCostCode")]
    public partial class TmpDivCostCode
    {
        [Key]
        public int Div { get; set; }
        public float? DivAmount { get; set; }
        [Column("blank")]
        public float? Blank { get; set; }
        [Column("1")]
        public float? _1 { get; set; }
        [Column("2")]
        public float? _2 { get; set; }
        [Column("3")]
        public float? _3 { get; set; }
        [Column("4")]
        public float? _4 { get; set; }
        [Column("5")]
        public float? _5 { get; set; }
        [Column("6")]
        public float? _6 { get; set; }
        [Column("7")]
        public float? _7 { get; set; }
        [Column("8")]
        public float? _8 { get; set; }
        [Column("9")]
        public float? _9 { get; set; }
        [Column("10")]
        public float? _10 { get; set; }
        [Column("11")]
        public float? _11 { get; set; }
        [Column("12")]
        public float? _12 { get; set; }
        [Column("13")]
        public float? _13 { get; set; }
        [Column("14")]
        public float? _14 { get; set; }
        [Column("15")]
        public float? _15 { get; set; }
        [Column("16")]
        public float? _16 { get; set; }
    }
}
