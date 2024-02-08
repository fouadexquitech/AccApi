using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class TechConditions
    {
        public int TcSeq { get; set; }
        public int? TcPackId { get; set; }
        public string TcDescription { get; set; }
        public string TcAccCondValue { get; set; }
        //public int groupId { get; set; }
        //public string groupDescription { get; set; }
        public List<TechConditionGroup> techConditionGroups { get; set; }
    }

    public class TechConditionGroup
    {
        public int groupId { get; set; }
        public string groupDescription { get; set; }
    }

    public class AccConditions
    {
        public int condId { get; set; }
        public string AccCondition { get; set; }
    }

    public class TechCondModel
    {
        public List<AccConditions> AccCondList { get; set; }
        public List<string> ListCC { get; set; }
}


}
