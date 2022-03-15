using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class CompManagementModel
    {
        public List<TopManagement> TopManagList { get; set; }
        public IFormFile ExcelComparisonSheet { get; set; }
    }
}
