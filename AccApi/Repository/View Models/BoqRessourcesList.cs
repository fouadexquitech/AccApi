using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class BoqRessourcesList
    {
        public short? RowNumber { get; set; }
        public string SectionO { get; set; }
        public string ItemO { get; set; }
        public string DescriptionO { get; set; }
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? UnitRate { get; set; }
        public int? Scope { get; set; }
        public string ObSheetDesc { get; set; }

        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? BurRev { get; set; }
        public int BoqSeq { get; set; }
        public string BoqCtg { get; set; }
        public string BoqUnitMesure { get; set; }
        public double? BoqQty { get; set; }
        public double? BoqUprice { get; set; }
        public string BoqDiv { get; set; }
        public string BoqPackage { get; set; }
        public int? BoqScope { get; set; }

        public string ResSeq { get; set; }
        public string ResDescription { get; set; }

    }
}
