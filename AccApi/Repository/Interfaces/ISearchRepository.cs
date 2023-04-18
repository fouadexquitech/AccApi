using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface ISearchRepository
    {
      
        List<BOQDivList> GetBOQDivList();
        List<BOQLevelList> GetBOQLevel2List();
        List<BOQLevelList> GetBOQLevel3List();
        List<BOQLevelList> GetBOQLevel4List();
        List<RESDivList> RESDivList();
        List<RESTypeList> RESTypeList();
        List<Package> PackageList();
        List<RESPackageList> RESPackageList();
        List<SheetDescList> SheetDescList();
    }
}
