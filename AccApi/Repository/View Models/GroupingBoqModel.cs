using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class GroupingBoqModel
    {
        public string ItemO { get; set; }
        public string BoqDiv { get; set; }
        public string DescriptionO  { get; set; }
        public string ObSheetDesc { get; set; }
        public int RowNumber { get; set; }
        public bool? ValidPerc { get; set; }
        public string? Unit { get; set; }
        public double? Qty { get; set; }
        public double? UnitPrice { get; set; }
        public double? TotalPrice { get; set; }  
        public bool IsSelected { get; set; }
        public double? QuotationQty { get; set; }
        public double? QuotationAmt { get; set; }

        public List<GroupingResourceModel>? GroupingResources { get; set; }
        public List<GroupingPackageSupplierPriceModel>? GroupingPackageSuppliersPrices { get; set; }
        public bool? IsAlternative { get; set; } = false;
        public bool? IsNewItem { get; set; } = false;
        public bool? IsExcluded { get; set; }=false;
        public string? LevelName { get; set; }
    }

    public class GroupingLevelModel
    {
        public string? LevelName { get; set; }

        public List<GroupingBoqModel> Items { get; set; }
    }
}
