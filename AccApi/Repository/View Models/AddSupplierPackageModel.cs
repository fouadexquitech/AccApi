
namespace AccApi.Repository.View_Models
{
    public class AddSupplierPackageModel
    {
        public int? SpPackSuppId { get; set; }
        public int? SpPackageId { get; set; }
        public int? SpSupplierId { get; set; }
        public byte? SpByBoq { get; set; }
        public bool? TecCondSent { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
    }
}
