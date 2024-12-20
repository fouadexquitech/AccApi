namespace AccApi.Repository.View_Models
{
    public class BoqResourceDT
    {
        public string? BoqItem { get; set; }

        public string? BoqCtg { get; set; }

        public string? ResDescription { get; set; }

        public string? BoqUnitMesure { get; set; }

        public double? BoqBillQty { get; set; }

        public double? BoqQty { get; set; }

        public double? BoqScopeQty { get; set; }

        public double? BoqUprice { get; set; }

        public double? TotalUnitPrice { get; set; }

        public bool? AssignedPackage { get; set; }
    }
}
