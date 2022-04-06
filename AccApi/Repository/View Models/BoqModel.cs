
using System;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class BoqModel
    {
        public int BoqSeq { get; set; }
        public string BoqCtg { get; set; }
        public string BoqUnitMesure { get; set; }
        public string BoqResSeq { get; set; }
        public double? BoqQty { get; set; }
        public double? BoqUprice { get; set; }
        public string BoqDiv { get; set; }
        public string BoqPackage { get; set; }
        public int? BoqScope { get; set; }
        public string ResDescription { get; set; }
        public string BoqItem { get; set; }
        
    }
}
