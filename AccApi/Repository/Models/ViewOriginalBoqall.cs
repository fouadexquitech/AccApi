using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewOriginalBoqall
    {
        [Required]
        [Column("Item-o")]
        [StringLength(25)]
        public string ItemO { get; set; }
        [Column("burRev")]
        public short BurRev { get; set; }
        [Column("burBackUpDate", TypeName = "datetime")]
        public DateTime BurBackUpDate { get; set; }
        [Column("Project-o")]
        [StringLength(10)]
        public string ProjectO { get; set; }
        [Column("section-o")]
        [StringLength(3)]
        public string SectionO { get; set; }
        [Column("Description-o")]
        [StringLength(1000)]
        public string DescriptionO { get; set; }
        [Column("Unit-o")]
        [StringLength(255)]
        public string UnitO { get; set; }
        [Column("Qty-o")]
        public double? QtyO { get; set; }
        public int? Area { get; set; }
        public int? Scope { get; set; }
        [StringLength(10)]
        public string Sort { get; set; }
        public bool? Subcontracting { get; set; }
        public bool? Selected { get; set; }
        [Column("obSheet")]
        public byte? ObSheet { get; set; }
        [Column("obInDirect")]
        public bool? ObInDirect { get; set; }
        [Column("obSheetDesc")]
        [StringLength(50)]
        public string ObSheetDesc { get; set; }
        public int? RowNumber { get; set; }
        [StringLength(10)]
        public string RefNumber { get; set; }
        [StringLength(12)]
        public string Zone { get; set; }
        [StringLength(10)]
        public string Prefix { get; set; }
        [Column("burQty")]
        public double? BurQty { get; set; }
        [Column("burUnitRate")]
        public double? BurUnitRate { get; set; }
        [Column("burSubmitted")]
        public double? BurSubmitted { get; set; }
        [Column("BOQTotalAmount")]
        public double BoqtotalAmount { get; set; }
        public bool? CandyTemplate { get; set; }
    }
}
