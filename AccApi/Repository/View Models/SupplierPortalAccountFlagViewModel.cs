using AccApi.Repository.Models.MasterModels;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class SupplierPortalAccountFlagViewModel
    {
        public List<int> Suppliers { get; set; }

        public bool AccountCreated { get; set; }
    }
}
