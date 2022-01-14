using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewBoq
    {
        [Column("boqSeq")]
        public int BoqSeq { get; set; }
        [Column("boqRivision")]
        public short? BoqRivision { get; set; }
        [Column("boqItem")]
        [StringLength(25)]
        public string BoqItem { get; set; }
        [Column("boqResSeq")]
        [StringLength(14)]
        public string BoqResSeq { get; set; }
        [Column("boqCtg")]
        [StringLength(1)]
        public string BoqCtg { get; set; }
        [Column("boqProject")]
        [StringLength(10)]
        public string BoqProject { get; set; }
        [Column("boqUnit")]
        [StringLength(10)]
        public string BoqUnit { get; set; }
        [Column("boqQty")]
        public double? BoqQty { get; set; }
        [Column("boqUPrice")]
        public double? BoqUprice { get; set; }
        [Column("boqProduction")]
        public double? BoqProduction { get; set; }
        [Column("boqCurrency")]
        [StringLength(3)]
        public string BoqCurrency { get; set; }
        [Column("boqWaste")]
        public double? BoqWaste { get; set; }
        [Column("boqWaste2")]
        public double? BoqWaste2 { get; set; }
        [Column("boqRisk")]
        public double? BoqRisk { get; set; }
        [Column("boqSupplier")]
        [StringLength(12)]
        public string BoqSupplier { get; set; }
        [Column("boqCC")]
        [StringLength(15)]
        public string BoqCc { get; set; }
        [Column("boqNote")]
        [StringLength(255)]
        public string BoqNote { get; set; }
        [Column("boqImportedMaterial")]
        public float? BoqImportedMaterial { get; set; }
        [Column("boqdiscount1")]
        public float? Boqdiscount1 { get; set; }
        [Column("boqdiscount2")]
        public float? Boqdiscount2 { get; set; }
        [Column("boqdiscount3")]
        public float? Boqdiscount3 { get; set; }
        [Column("boqNetPrice")]
        public float? BoqNetPrice { get; set; }
        [Column("boqDiv")]
        [StringLength(2)]
        public string BoqDiv { get; set; }
        [Column("boqSubDiv")]
        [StringLength(3)]
        public string BoqSubDiv { get; set; }
        [Column("boqTrade")]
        [StringLength(5)]
        public string BoqTrade { get; set; }
        [Column("boqSubTrade")]
        [StringLength(5)]
        public string BoqSubTrade { get; set; }
        [Column("boqSubID")]
        [StringLength(12)]
        public string BoqSubId { get; set; }
        [Column("boqDiscOverRide")]
        public byte? BoqDiscOverRide { get; set; }
        [Column("boqSheet")]
        public byte? BoqSheet { get; set; }
        [Column("boqBackUpDate", TypeName = "datetime")]
        public DateTime? BoqBackUpDate { get; set; }
        public double? UnitPrice { get; set; }
        [Column("boqCandyTemplate")]
        public bool? BoqCandyTemplate { get; set; }
    }
}
