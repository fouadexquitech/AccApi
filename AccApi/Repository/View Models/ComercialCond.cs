using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class SupplierInput
    {
        public int supID { get; set; }
    }

    public class ComercialCond
    {
        public int id { get; set; }
        public string description { get; set; }
    }

    public class SupplierInputList
    {
        public List<SupplierInput> supplierInputList { get; set; }
        public List<ComercialCond> comercialCondList { get; set; }
    }
}
