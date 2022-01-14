using System;
using System.Collections.Generic;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class TbSupplierPackage
    {
        public int PsId { get; set; }
        public int? PsPackId { get; set; }
        public int? PsSuppId { get; set; }
    }
}
