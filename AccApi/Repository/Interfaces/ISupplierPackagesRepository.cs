using System;
using System.Collections.Generic;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Http;

namespace AccApi.Repository.Interfaces
{
    public interface ISupplierPackagesRepository 
    {
        List<SupplierPackagesList> SupplierPackagesList(int packageid);

        SupplierPackagesList GetSupplierPackage(int spId);

        string ValidateExcelBeforeAssign(int packId, byte byBoq);
        bool AssignPackageSuppliers(int packId,List<SupplierInputList> supInputList,string FilePath, byte ByBoq);
    }
}
