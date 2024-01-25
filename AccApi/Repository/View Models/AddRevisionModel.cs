using System;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class AddRevisionModel
    {
        public int? PrRevId { get; set; }
        public int? PrRevNo { get; set; }
        public DateTime? PrRevDate { get; set; }
        public decimal? PrTotPrice { get; set; }
        public int? PrPackSuppId { get; set; }
        public int? PrCurrency { get; set; }
        public double? PrExchRate { get; set; }
        public int? StatusId { get; set; }
        public string? ProjectCode { get; set; }
        public bool? IsSynched { get; set; } = false;
        public List<AddRevisionDetailModel> RevisionDetails { get; set; }

        //List of commercial conditions
        public List<AddCondModel> CommercialConditions { get; set; }

        //List of technical conditions
        public List<AddCondModel> TechnicalConditions { get; set; }
    }
}
