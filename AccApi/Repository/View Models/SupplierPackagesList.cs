using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class SupplierPackagesList
    {
        public int PsId { get; set; }
        public int? PsPackId { get; set; }
        public int? PsSuppId { get; set; }
        public byte? PsByBoq { get; set; }
        public string? PsSupName { get; set; }
    }
}
