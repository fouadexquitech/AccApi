using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface IPackageRepository
    {
        List<BoqRessourcesList> GetOriginalBoqList(SearchInput input, string costDB);
        List<BoqModel> GetBoqList(string ItemO, SearchInput input); 
        List<BoqModel> GetAllBoqList(SearchInput input);
        string ExportBoqExcel(AssignPackages input);
        public bool updateOriginalBoqQty(OriginalBoqModel boq);
        public bool updateBoqResQty(BoqModel res);
        public bool updateBoqTradeDesc(string tradeDesc, List<OriginalBoqModel> origBoqList);

        PackageDetailsModel GetPackageById(int IdPkge);
        bool AssignPackages(AssignPackages input);
        List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge, SearchInput input);
        List<Package> GetPackages(string filter);
        bool AddPackage(List<Package> packs);
        bool UpdatePackage(Package pack);
        bool DeletePackage(int id);

        string ExportExcelPackagesCost();
    }
}
