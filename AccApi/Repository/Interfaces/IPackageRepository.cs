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
        Task<string> ExportExcelVerification(SearchInput input, string costDB, string userName);
        Task<string> ExportNotAssigned(string costDB);
        public bool updateOriginalBoqQty(OriginalBoqModel boq);
        public bool updateBoqResQty(BoqModel res);
        public bool updateBoqTradeDesc(string tradeDesc, List<OriginalBoqModel> origBoqList);

        PackageDetailsModel GetPackageById(int IdPkge);
        bool AssignPackages(AssignPackages input);
        List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge, SearchInput input);
        DataTablesResponse<Package> GetPackages(DataTablesRequest Request);
        bool AddPackage(List<Package> packs);
        bool UpdatePackage(Package pack);
        Task<ResponseModel<bool>> DeletePackage(int id);
        Task<string> ExportExcelPackagesCost(int withBoq,string costDB, SearchInput input);
        Task<DataTablesResponse<BoqModel>> GetBoqResourceRecords(DataTablesRequest dtRequest);
    }
}
