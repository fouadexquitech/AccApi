using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class AssignRevisionDetails
    {
        public int resourceID { get; set; }
        public string resourceName { get; set; }
       
        public int? GroupId { get; set; }
        public string resourceUnit { get; set; }
        public double? resourceQty { get; set; }
        public double? price { get; set; }
        public string boqItem { get; set; }
        public double? boqQty { get; set; }
        public double? assignpercent { get; set; }
        public double? assignQty { get; set; }
        public double? assignPrice { get; set; }
        public int revisionId { get; set; }
        public double? priceOrigCurrency { get; set; }
    }

    public class SupplierPercent
    {
        public int supID { get; set; }
        public double percent { get; set; }
      
    }

    public class SupplierQty
    {
        public int supID { get; set; }
        public double qty { get; set; }

    }

    public class SupplierResrouces
    {
        public int resourceID { get; set; }
        public List<SupplierQty> supplierQtys { get; set; }
        public List<SupplierPercent> supplierPercents { get; set; }
    }

    public class SupplierBOQ
    {
        public string BoqItemID { get; set; }
        public List<SupplierPercent> supplierPercents { get; set; }
        public List<SupplierQty> supplierQtys { get; set; }
    }

    public class SupplierGroups
    {
        public int GroupId { get; set; }
        public List<SupplierPercent> supplierPercents { get; set; }
        public List<SupplierQty> supplierQtys { get; set; }
    }

    public class boqItem
    {
        public string BoqItemID { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
    }

    public class ressourceItem
    {
        public int resId { get; set; }
    }

    public class AssignSuppliertBoq
    {
        public List<SupplierQty> supplierQtyList { get; set; }
        public List<SupplierPercent> supplierPercentList { get; set; }
        public List<boqItem> supplierBoqItemList { get; set; }
    }

    public class AssignSupplierGroup
    {
        public List<SupplierPercent> supplierPercentList { get; set; }
        public List<Group> supplierGroupList { get; set; }
        public List<SupplierQty> supplierQtyList { get; set; }
    }

    public class AssignSuppliertRes
    {
        public List<SupplierQty> supplierQtyList { get; set; }
        public List<SupplierPercent> supplierPercentList { get; set; }
        public List<ressourceItem> supplierResItemList { get; set; }

    }

    public class Ressource
    {
        public string seq { get; set; }

        public string ItemO { get; set; }
    }

    public class RessourceLevelsFilter
    {
        public List<string> Level2 { get; set; }

        public List<string> Level3 { get; set; }

        public List<string> Level4 { get; set; }
    }
}
