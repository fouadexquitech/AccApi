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

        List<IFormFile> ListFileAttachments { get; set; }

        public string UserName { get; set; }
    }

    public class AssignPackageTemplateModel
    {
        public SupplierInput supplierInput { get; set; }
        public string EmailTemplate { get; set; }
        public string FilePath { get; set; }
        public List<ComercialCond> comercialCondList { get; set; }

        public List<string> mailCC { get; set; }
        public List<string> mailAttachments { get; set; }
        public List<IFormFile> ListFileAttachments { get; set; }
        public string UserName { get; set; }
    }
}
