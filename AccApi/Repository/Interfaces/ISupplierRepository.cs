using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        List<SupplierList> SupplierList(int packID);
    }
}
