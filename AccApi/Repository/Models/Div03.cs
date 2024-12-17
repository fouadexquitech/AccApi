using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class Div03
    {
        [Required]
        [Column("Item-o")]
        [StringLength(25)]
        public string ItemO { get; set; }
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
        public double? Submitted { get; set; }
        [Column("obSheet")]
        public byte? ObSheet { get; set; }
        [Column("obInDirect")]
        public bool? ObInDirect { get; set; }
        [Column("obSeq")]
        public int ObSeq { get; set; }
        [Column("obSheetDesc")]
        [StringLength(50)]
        public string ObSheetDesc { get; set; }
        public short? RowNumber { get; set; }
        [StringLength(10)]
        public string RefNumber { get; set; }
        public double? UnitRate { get; set; }
        [StringLength(12)]
        public string Zone { get; set; }
        public bool? CandyTemplate { get; set; }
        [Column("obBOQSellRate")]
        public double? ObBoqsellRate { get; set; }
        [Column("obBOQSellTotPrice")]
        public double? ObBoqsellTotPrice { get; set; }
    }
}
