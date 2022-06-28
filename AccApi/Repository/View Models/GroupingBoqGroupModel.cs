using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class GroupingBoqGroupModel
    {
        public int Id { get; set; }
        public string Name  { get; set; }
        public double? TotalQty { get; set; }
        public double? TotalPrice { get; set; }
        public bool? ValidPerc { get; set; }
        public List<GroupingPackageSupplierPriceModel>? GroupingPackageSuppliersPrices { get; set; }
    }
}

