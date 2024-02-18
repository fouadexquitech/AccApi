using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class ComConditions
    {
        public int cmSeq { get; set; }
        public string cmDescription { get; set; }
        public string cmAccCondValue { get; set; }
        public bool? Checked { get; set; }
    }
}
