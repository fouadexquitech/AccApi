﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class AssignRevisionDetails
    {
        public int resourceID { get; set; }
        public string resourceName { get; set; }
       
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

    public class SupplierResrouces
    {
        public int resourceID { get; set; }
        public List<SupplierPercent> supplierPercents { get; set; }
    }

    public class SupplierBOQ
    {
        public string BoqItemID { get; set; }
        public List<SupplierPercent> supplierPercents { get; set; }
    }

    public class boqItem
    {
        public string BoqItemID { get; set; }
    }

    public class ressourceItem
    {
        public int resId { get; set; }
    }

    public class AssignSuppliertBoq
    {
        public List<SupplierPercent> supplierPercentList { get; set; }
        public List<boqItem> supplierBoqItemList { get; set; }
    }

    public class AssignSuppliertRes
    {
        public List<SupplierPercent> supplierPercentList { get; set; }
        public List<ressourceItem> supplierResItemList { get; set; }
    }
}
