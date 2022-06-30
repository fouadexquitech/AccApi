using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class TopManagementTemplateModel
    {
        public int PackageId { get; set; }
        public string Template { get; set; }
        public List<TopManagement> TopManagements { get; set; }
        public List<string> ListCC { get; set; }
        public List<string> ListAttach { get; set; }
        public string UserName { get; set; }
    }

    public class AssignPackageTemplateModel
    {
        public List<SupplierInputList> supInputList { get; set; }
        public int packId { get; set; }
        public byte ByBoq { get; set; }
        public string UserName { get; set; }
        public List<string> ListCC { get; set; }
        public List<string> ListAttach { get; set; }
    }
}
