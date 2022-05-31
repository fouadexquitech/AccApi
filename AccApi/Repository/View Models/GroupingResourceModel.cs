﻿using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class GroupingResourceModel
    {
        public int BoqSeq { get; set; }  
        public string ResourceSeq { get; set; }
        public string ResourceDescription { get; set; }
        public string Unit { get; set; }
        public double? UnitPrice { get; set; }
        public double? Qty { get; set; }
        public double? TotalPrice { get; set; }
        public bool IsSelected { get; set; }
        public bool ValidPerc { get; set; }
        public List<GroupingPackageSupplierPriceModel>? GroupingPackageSuppliersPrices { get; set; }
    }
}
