using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class RevisionDetailsList
    {
        public int RdRevisionId { get; set; }
      
        public int RdResourceSeq { get; set; }

        public double? RdPrice { get; set; }
    }
}
