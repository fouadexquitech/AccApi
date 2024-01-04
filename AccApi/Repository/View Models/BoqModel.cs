
using System;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class BoqModel
    {
        public int? RowNumber { get; set; }
        public string SectionO { get; set; }
        public string ItemO { get; set; }
        public string DescriptionO { get; set; }
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? BillQtyO { get; set; }
        public double? ScopeQtyO { get; set; }
        public double? UnitRate { get; set; }
        public int? Scope { get; set; }
        public string ObSheetDesc { get; set; }
        public string ObTradeDesc { get; set; }
        public string L2 { get; set; }
        public string L3 { get; set; }
        public string L4 { get; set; }


        public int BoqSeq { get; set; }
        public string BoqCtg { get; set; }
        public string BoqUnitMesure { get; set; }
        public string BoqResSeq { get; set; }
        public double? BoqUprice { get; set; }
        public string BoqDiv { get; set; }
        public string BoqPackage { get; set; }
        public int? BoqScope { get; set; }
        public string ResDescription { get; set; }
        public string BoqItem { get; set; }
        public double? BoqTotalPrice { get; set; }
        public string? AssignedPackage { get; set; }
        public double? BoqQty { get; set; }
        public double? BoqBillQty { get; set; }
        public double? BoqScopeQty { get; set; }

        public bool IsSelected { get; set; }

        public double? TotalUnitPrice { get; set; }
        

    }
}
