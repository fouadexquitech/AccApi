using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class AddSupplierPackageRevisionModel
    {
        public List<AddSupplierPackageModel> SupplierPackageModels { get; set; }
        public List<AddRevisionModel> RevisionModels { get; set; }
    }
}
