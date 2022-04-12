using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class GroupingGroupModel
    {   int GroupId { get; set; }
        string GroupName { get; set; }
        double? TotalPrice { get; set; }
        public bool ValidPerc { get; set; }
        public List<GroupingPackageSupplierPriceModel>? GroupingPackageSuppliersPrices { get; set; }
    }
}
