using AccApi.Repository.View_Models;
using System.Collections.Generic;

namespace AccApi.Repository.Interfaces
{
   public interface ISupplierPackagesRevRepository
    {
        List<SupplierPackagesRevList> GetSupplierPackagesRevList(int PackageSupplierId, string CostConn);

        SupplierPackagesRevList GetSupplierPackagesRevision(int revisionId, string CostConn);
        decimal? AddField(int revId, string lbl, double val,int type, string CostConn);
        List<CurrencyList> GetCurrencies();
        bool DeleteField(int fieldId, string CostConn);
        List<RevisionFieldsList> GetFields(int revisionid, string CostConn);
    }
}
