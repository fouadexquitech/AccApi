using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tmpBoqRessources")]
    public partial class TmpBoqRessource
    {
        public int? RowNumber { get; set; }
        [StringLength(50)]
        public string SectionO { get; set; }
        [StringLength(50)]
        public string ItemO { get; set; }
        [StringLength(1000)]
        public string DescriptionO { get; set; }
        [StringLength(50)]
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? BillQtyO { get; set; }
        public double? ScopeQtyO { get; set; }
        public double? UnitRate { get; set; }
        public int? Scope { get; set; }
        [StringLength(50)]
        public string ObSheetDesc { get; set; }
        [StringLength(50)]
        public string ObTradeDesc { get; set; }
        [StringLength(5000)]
        public string L1 { get; set; }
        [StringLength(5000)]
        public string L2 { get; set; }
        [StringLength(5000)]
        public string L3 { get; set; }
        [StringLength(5000)]
        public string L4 { get; set; }
        [StringLength(5000)]
        public string L5 { get; set; }
        public int? BoqSeq { get; set; }
        [StringLength(50)]
        public string BoqCtg { get; set; }
        [StringLength(50)]
        public string BoqUnitMesure { get; set; }
        [StringLength(50)]
        public string BoqDiv { get; set; }
        public double? BoqUprice { get; set; }
        public double? BoqQty { get; set; }
        public double? BoqBillQty { get; set; }
        public double? BoqScopeQty { get; set; }
        public int? BoqScope { get; set; }
        [StringLength(5000)]
        public string ResDescription { get; set; }
        [StringLength(5000)]
        public string AssignedPackage { get; set; }
        [StringLength(50)]
        public string BoqStatus { get; set; }
        public double? BoqTotalPrice { get; set; }
    }
}
