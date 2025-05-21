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
        public string? C { get; set; }
    }

    public class GroupingLevelModel
    {
//AH21052025
        public string? C_Description { get; set; }
        public double? C_TotalBudget { get; set; }
        public List<GroupingPackageSupplierPriceModel>? GroupingSupplierC_Prices { get; set; }
//AH21052025
        public List<GroupingBoqModel> Items { get; set; }
        public string? LevelName { get; set; }
    }

    public class SupplierC_Price
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public double? SupplierTotalPrice { get; set; }
    }

 }
