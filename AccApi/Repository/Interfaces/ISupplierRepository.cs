using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        List<Supplier> SupplierList(int packID);
        
        DataTablesResponse<Supplier> GetSuppliers(DataTablesRequest dtRequest);

        bool AddSupplier(List<Supplier> sups);
        bool UpdateSupplier(Supplier sup);
        bool DeleteSupplier(int id);

        Task<bool> UpdatePortalAccountFlag(SupplierPortalAccountFlagViewModel model);

        List<Supplier> GetSupplierList_NotAssignetPackage(int packID, string CostConn);

        Task<ResponseModel<bool>> Register(List<RegisterModel> model);
    }
}
