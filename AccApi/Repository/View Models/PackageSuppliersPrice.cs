﻿using System;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class PackageSuppliersPrice
    {
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public List<RevisionDetails> revisionDetails { get; set; }
        public List<FieldList> fieldLists { get; set; }
        public decimal totalprice { get; set; }

        public decimal totalAdditionalPrice { get; set; }

        public decimal totalNetPrice { get; set; }

        public byte ByBoq { get; set; }

        public DateTime? LastRevisionDate { get; set; }

        public string? RevisionCurrency { get; set; }
    }

    public class FieldList
    {
        public string Label { get; set; }
        public double Value { get; set; }
        public int? Type { get; set; }
        public string? OriginalCurrency { get; set; }
    }

    public class RevisionDetails
    {
        public int? RowNumber { get; set; }
        public string SectionO { get; set; }
        public string ItemO { get; set; }
        public string DescriptionO { get; set; }
        public string UnitO { get; set; }
        public double? QtyO { get; set; }
        public double? UnitRate { get; set; }
        public int? Scope { get; set; }
        public string ObSheetDesc { get; set; }

        public int BoqSeq { get; set; }
        public string BoqCtg { get; set; }
        public string BoqUnitMesure { get; set; }
        public double? BoqQty { get; set; }
        public double? BoqUprice { get; set; }
        public string BoqDiv { get; set; }
        public string BoqPackage { get; set; }
        public int? BoqScope { get; set; }
        public string ResDescription { get; set; }
        public string resourceID { get; set; }
        public string resourceUnit { get; set; }
        public double? resourceQty { get; set; }
        public double? price { get; set; }
        public double? perc { get; set; }
        public double? priceOrigCur { get; set; }
        public string? OriginalCurrency { get; set; }
        public byte? missedPrice { get; set; }
        public string ResDiv { get; set; }
        public string ResCtg{ get; set; }

        public bool AssignedToSupplier { get; set; }
        public double? AssignedQty { get; set; }

        public double? Discount { get; set; }
        public double? UPriceAfterDiscount { get; set; }

        public decimal totalPriceAfterExchange { get; set; }

        public bool? IsAlternative { get; set; } = false;
        public bool? IsNewItem { get; set; } = false;
        public int? NewItemId { get; set; }
        public int? NewItemResourceId { get; set; }
        public string? ParentItemO { get; set; }
        public int? ParentResourceId { get; set; }
        public bool? IsExcluded { get; set; } = false;
        public int SupplierId { get; set; }
    }
}
