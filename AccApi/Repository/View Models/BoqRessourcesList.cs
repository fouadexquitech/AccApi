using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class BoqRessourcesList
    {
        public int? RowNumber { get; set; }
        public string SectionO { get; set; }
        public string ItemO { get; set; }
        public string DescriptionO { get; set; }
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? BillQtyO { get; set; }
        public double? ScopeQtyO { get; set; }
        public double? UnitRateO { get; set; }
        public double? TotalPriceO { get; set; }
        public int? ScopeO { get; set; }
        public string ObSheetDesc { get; set; }
        public string ObTradeDesc { get; set; }
        public string? AssignedPackage { get; set; }
       public string BoqStatus { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }

        public int? BurRev { get; set; }
        public int BoqSeq { get; set; }
        public string BoqCtg { get; set; }
        public string BoqUnitMesure { get; set; }
        public double? BoqUprice { get; set; }
        public string BoqDiv { get; set; }
        public string BoqSubDiv { get; set; }
        public string BoqTrade { get; set; }
        public string BoqPackage { get; set; }
        public int? BoqScope { get; set; }
        public string ResSeq { get; set; }
        public string ResDescription { get; set; }
        public string BoqItem { get; set; }
        public double? BoqQty { get; set; }
        public double? BoqBillQty { get; set; }
        public double? BoqScopeQty { get; set; }
        public double? BoqTotalPrice { get; set; }
        public string BoqResSeq { get; set; }
        public string resCode { get; set; }

        public bool? IsAlternative { get; set; } = false;
        public bool? IsNewItem { get; set; } = false;
        public int? NewItemId { get; set; }
        public int? NewItemResourceId { get; set; }
        public string? ParentItemO { get; set; }
        public int? ParentResourceId { get; set; }
        public bool? IsExcluded { get; set; } = false;
        public int SupplierId { get; set; }

        public string? L1 { get; set; } = "";
        public string? L2 { get; set; } = "";
        public string? L3 { get; set; } = "";
        public string? L4 { get; set; } = "";
        public string? L5 { get; set; } = "";
        public string? L6 { get; set; } = "";
        public string? L7 { get; set; } = "";
        public string? L8 { get; set; } = "";
        public string? L9 { get; set; } = "";
        public string? L10 { get; set; } = "";
        public string? C1 { get; set; } = "";
        public string? C2 { get; set; } = "";
        public string? C3 { get; set; } = "";
        public string? C4 { get; set; } = "";
        public string? C5 { get; set; } = "";
        public string? C6 { get; set; } = "";
        public string C7 { get; set; } = "";
        public string C8 { get; set; } = "";
        public string C9 { get; set; } = "";
        public string C10 { get; set; } = "";
        public string C11 { get; set; } = "";
        public string C12 { get; set; } = "";
        public string C13 { get; set; } = "";
        public string C14 { get; set; } = "";
        public string C15 { get; set; } = "";
        public string? LevelName { get; set; } = "";

        public string BoqCtg_st { get; set; }
        public string BoqUnitMesure_st { get; set; }
        public double? BoqUprice_st { get; set; }
        public string BoqDiv_st { get; set; }
        public double? BoqQty_st { get; set; }
        public double? BoqTotalPrice_st { get; set; }

    }


    public class packagesList
    {
        public int? PkgeId { get; set; }
        public string? PkgeName { get; set; }
        public double? TotalBudget { get; set; }
        public double? TotalBudgetBeforeDisct { get; set; }
    }
}
