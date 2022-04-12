using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface IComparisonGroupRepository
    {
        bool AddGroup(ComparisonPackageGroupModel ComparisonPackageGroup);

        List<ComparisonPackageGroupModel> GetGroups(int packageId);

        List<GroupingBoqModel> GetBoqList(int packageId, int groupId, SearchInput input);

        List<GroupingBoqModel> GetBoqListOnly(int packageId, int groupId, SearchInput input);


        bool AttachToGroup(int groupId, List<GroupingResourceModel> list);

        bool DetachFromGroup(int groupId, List<GroupingResourceModel> list);

        bool AttachToGroupByBoq(int groupId, List<GroupingBoqModel> list);

        bool DetachFromGroupByBoq(int groupId, List<GroupingBoqModel> list);

    }
}
