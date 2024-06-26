﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class SupplierPackagesRevList
    {
        public int PrRevId { get; set; }
        public int? PrRevNo { get; set; }
        public DateTime? PrRevDate { get; set; }
        public decimal? PrTotPrice { get; set; }
        public int? PrPackSuppId { get; set; }
        public int? PrCurrency { get; set; }
        public double? PrExchRate { get; set; }

        public string? Currency { get; set; }
        public DateTime? PrRevExpDate { get; set; }
    }
}
