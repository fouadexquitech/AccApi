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
        List<BoqModel> GetBoqList(string ItemO, string CostConn, SearchInput input); 
        List<BoqModel> GetAllBoqList(string CostConn,SearchInput input);
        Task<string> ExportBoqExcel(SearchInput input, string costDB);
        Task<string> ExportExcelVerification(SearchInput input, string costDB, string userName);
        Task<string> ExportNotAssigned(string costDB);
        public bool updateOriginalBoqQty(string CostConn,OriginalBoqModel boq);
        public bool updateBoqResQty(string CostConn, BoqModel res);
        public bool updateBoqTradeDesc(string tradeDesc, string CostConn, List<OriginalBoqModel> origBoqList);

        PackageDetailsModel GetPackageById(int IdPkge);
        bool AssignPackages(AssignPackages input, string CostConn);
        List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge, SearchInput input, string CostConn);
        DataTablesResponse<Package> GetPackages(DataTablesRequest Request);
        bool AddPackage(List<Package> packs);
        bool UpdatePackage(Package pack);
        Task<ResponseModel<bool>> DeletePackage(int id, string CostConn);
        Task<string> ExportExcelPackagesCost(int withBoq,string costDB,string CostConn, SearchInput input);
        Task<DataTablesResponse<BoqModel>> GetBoqResourceRecords(string CostConn,DataTablesRequest dtRequest);
    }
}
