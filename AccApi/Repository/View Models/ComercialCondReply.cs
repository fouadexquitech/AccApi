using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{


    public class ConditionsReply
    {
        public int condId { get; set; }
        public string condDesc { get; set; }
        public string condReply { get; set; }
        public int supId { get; set; }
        public string supName { get; set; }
        public string accCondValue { get; set; }

    }

}
