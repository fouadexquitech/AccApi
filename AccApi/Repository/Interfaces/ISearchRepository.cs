using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface ISearchRepository
    {
      
        List<BOQDivList> GetBOQDivList();
        List<BOQLevelList> GetBOQLevel2List(RessourceLevelsFilter filter);
        //List<BOQLevelList> GetBOQLevel2ListBy(RessourceLevelsFilter filter);
        List<BOQLevelList> GetBOQLevel3List(RessourceLevelsFilter filter);
        List<BOQLevelList> GetBOQLevel4List(RessourceLevelsFilter filter);
        List<RESDivList> RESDivList();
        List<RESTypeList> GetResTypeList(RessourceLevelsFilter filter);
        List<Package> PackageList();
        List<RESPackageList> RESPackageList();
        List<SheetDescList> SheetDescList();
        List<RessourceList> GetRessourcesList(RessourceLevelsFilter filter);
        List<BOQLevelList> GetBOQLevel3ListByLevel2(RessourceLevelsFilter filter);
        List<BOQLevelList> GetBOQLevel4ListByLevel3(RessourceLevelsFilter filter);
        List<RessourceList> GetRessourcesListByLevels(RessourceLevelsFilter filter);
    }
}
