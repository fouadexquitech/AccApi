namespace AccApi.Repository.View_Models
{
    public class DisplayCondReply
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }

        public int? ConditionId { get; set; }

        public string? Reply { get; set; }
        public string? AccCondValue { get; set; }
    }
}
