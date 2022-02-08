using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
   public interface ISupplierPackagesRevRepository
    {
        List<SupplierPackagesRevList> GetSupplierPackagesRevList(int PackageSupplierId);
        decimal? AddField(int revId, string lbl, int val,int type);
        List<CurrencyList> GetCurrencies();
        bool DeleteField(int fieldId);
        List<RevisionFieldsList> GetFields(int revisionid);
    }
}
