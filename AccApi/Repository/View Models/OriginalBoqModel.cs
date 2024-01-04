using System;

namespace AccApi.Repository.View_Models
{
    public class OriginalBoqModel
    {
        public int? RowNumber { get; set; }
        public string SectionO { get; set; }
        public string ItemO { get; set; }
        public string DescriptionO { get; set; }
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? UnitRate { get; set; }
        public int? Scope { get; set; }
        public string Package { get; set; }
        public double? ScopeQtyO { get; set; }
        public double? BillQtyO { get; set; }
        public string ObTradeDesc { get; set; }
        
    }
}
