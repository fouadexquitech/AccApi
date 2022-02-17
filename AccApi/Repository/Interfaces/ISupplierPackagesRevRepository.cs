using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
   public interface ISupplierPackagesRevRepository
    {
        List<SupplierPackagesRevList> GetSupplierPackagesRevList(int PackageSupplierId);

        SupplierPackagesRevList GetSupplierPackagesRevision(int revisionId);
        decimal? AddField(int revId, string lbl, double val,int type);
        List<CurrencyList> GetCurrencies();
        bool DeleteField(int fieldId);
        List<RevisionFieldsList> GetFields(int revisionid);
    }
}
