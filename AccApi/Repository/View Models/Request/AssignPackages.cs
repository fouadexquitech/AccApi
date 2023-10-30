using System.Collections.Generic;

namespace AccApi.Repository.View_Models.Request
{
    public class AssignPackages
    {
        public List<AssignOriginalBoqList> AssignOriginalBoqList { get; set; }
        public List<AssignBoqList> AssignBoqList { get; set; }
        public List<int> BoqSeqs { get; set; }

        public int PackageId { get; set; }
    }
}
