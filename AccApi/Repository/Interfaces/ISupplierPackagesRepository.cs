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
        string ValidateExcelBeforeAssign(int packId);
        bool AssignPackageSuppliers(int packId, List<SupplierInput> supList,string FilePath, string EmailContent, byte ByBoq);
    }
}
