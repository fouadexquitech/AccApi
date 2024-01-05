using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Common;
using AccApi.Repository.View_Models.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface IPackageRepository
    {
        Task<List<BoqRessourcesList>> GetOriginalBoqList(SearchInput input, string costDB);
        List<BoqModel> GetBoqList(string ItemO, SearchInput input); 
        List<BoqModel> GetAllBoqList(SearchInput input);
        Task<string> ExportBoqExcel(SearchInput input, string costDB);
        Task<string> ExportNotAssigned(SearchInput input, string costDB);
        public bool updateOriginalBoqQty(OriginalBoqModel boq);
        public bool updateBoqResQty(BoqModel res);
        public bool updateBoqTradeDesc(string tradeDesc, List<OriginalBoqModel> origBoqList);

        PackageDetailsModel GetPackageById(int IdPkge);
        bool AssignPackages(AssignPackages input);
        List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge, SearchInput input);
        DataTablesResponse<Package> GetPackages(DataTablesRequest Request);
        bool AddPackage(List<Package> packs);
        bool UpdatePackage(Package pack);
        bool DeletePackage(int id);
        string ExportExcelPackagesCost(int withBoq);
        Task<DataTablesResponse<BoqModel>> GetBoqResourceRecords(DataTablesRequest dtRequest);
    }
}
