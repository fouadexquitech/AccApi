using System;

namespace AccApi.Repository.View_Models
{
    public class OriginalBoqModel
    {
        public short? RowNumber { get; set; }
        public string SectionO { get; set; }
        public string ItemO { get; set; }
        public string DescriptionO { get; set; }
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? UnitRate { get; set; }
        public int? Scope { get; set; }
    }
}
