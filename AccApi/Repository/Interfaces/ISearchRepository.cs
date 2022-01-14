using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface ISearchRepository
    {
        List<BOQDivList> BOQDivList();
        List<RESDivList> RESDivList();
        List<RESTypeList> RESTypeList();
        List<PackageList> PackageList();
        List<RESPackageList> RESPackageList();
        List<SheetDescList> SheetDescList();
    }
}
