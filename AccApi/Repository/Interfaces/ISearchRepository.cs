using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface ISearchRepository
    {
      
        List<BOQDivList> GetBOQDivList(RessourceLevelsFilter filter, string CostConn);
        List<BOQLevelList> GetBOQLevel2List(RessourceLevelsFilter filter, string CostConn);
        //List<BOQLevelList> GetBOQLevel2ListBy(RessourceLevelsFilter filter);
        List<BOQLevelList> GetBOQLevel3List(RessourceLevelsFilter filter, string CostConn);
        List<BOQLevelList> GetBOQLevel4List(RessourceLevelsFilter filter, string CostConn);
        List<RESDivList> RESDivList(string CostConn);
        List<RESTypeList> GetResTypeList(RessourceLevelsFilter filter, string CostConn);
        List<Package> GetPackagesList(bool usedPackages, string CostConn);
        List<RESPackageList> RESPackageList(string CostConn);
        List<SheetDescList> SheetDescList( string CostConn);
        List<RessourceList> GetRessourcesList(RessourceLevelsFilter filter, string CostConn);
        List<BOQLevelList> GetBOQLevel3ListByLevel2(RessourceLevelsFilter filter, string CostConn);
        List<BOQLevelList> GetBOQLevel4ListByLevel3(RessourceLevelsFilter filter, string CostConn);
        List<RessourceList> GetRessourcesListByLevels(RessourceLevelsFilter filter, string CostConn);
    }
}
