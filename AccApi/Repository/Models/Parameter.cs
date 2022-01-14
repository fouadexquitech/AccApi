using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class Parameter
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        public double? Per { get; set; }
        public double? B1 { get; set; }
        public double? B2 { get; set; }
        public double? B3 { get; set; }
        public double? B4 { get; set; }
        public double? B5 { get; set; }
        public double? L1 { get; set; }
        public double? L2 { get; set; }
        public double? L3 { get; set; }
        public double? L4 { get; set; }
        public double? L5 { get; set; }
        public double? M1 { get; set; }
        public double? M2 { get; set; }
        public double? M3 { get; set; }
        public double? M4 { get; set; }
        public double? M5 { get; set; }
        public double? S1 { get; set; }
        public double? S2 { get; set; }
        public double? S3 { get; set; }
        public double? S4 { get; set; }
        public double? S5 { get; set; }
        public double? E1 { get; set; }
        public double? E2 { get; set; }
        public double? E3 { get; set; }
        public double? E4 { get; set; }
        public double? E5 { get; set; }
    }
}
