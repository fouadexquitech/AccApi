using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblIndirectCostHistogram")]
    public partial class TblIndirectCostHistogram
    {
        public short? Month { get; set; }
        [StringLength(25)]
        public string Indexx { get; set; }
        [StringLength(25)]
        public string Grp { get; set; }
        public byte? Ctg { get; set; }
        public double? Total { get; set; }
        public double? MonthlyCost { get; set; }
        public double? MonthlyCostLabor { get; set; }
        public double? Fuel { get; set; }
        public double? Parts { get; set; }
        public double? Depreciation { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        public short? Rivision { get; set; }
        [Column("isLabor")]
        public bool? IsLabor { get; set; }
        public float? LaborCost { get; set; }
        public float? NonLaborCost { get; set; }
        public short? MaxPeriode { get; set; }
    }
}
