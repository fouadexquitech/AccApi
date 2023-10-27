using AccApi.Repository.View_Models.Request;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models.Common
{
    public class DataTablesRequest
    {
        public int Start { get; set; }

        public int Length { get; set; }
        
        public string SortDirVal { get; set; }

        public string SortCol { get; set; }
        
        public string? SearchVal { get; set; }

        public List<string> BoqItems { get; set; }

        public List<string> SelectedBoqItems { get; set; }

        public SearchInput input { get; set; } = new SearchInput();


    }
}
