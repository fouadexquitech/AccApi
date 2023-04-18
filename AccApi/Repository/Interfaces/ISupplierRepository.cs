using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        List<Supplier> SupplierList(int packID);
        List<Supplier> GetSuppliers(string filter);

        bool AddSupplier(List<Supplier> sups);
        bool UpdateSupplier(Supplier sup);
        bool DeleteSupplier(int id);
    }
}
