using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Http;

namespace AccApi.Repository.Interfaces
{
    public interface ISupplierPackagesRepository 
    {
        List<SupplierPackagesList> GetSupplierPackagesList(int packageid);

        SupplierPackagesList GetSupplierPackage(int spId);

        string ValidateExcelBeforeAssign(int packId, byte byBoq, bool withPrice);
        Task<bool> AssignPackageSuppliers(int packId,List<SupplierInputList> supInputList, byte ByBoq, string UserName, List<IFormFile> attachments,DateTime RevExpiryDate);
        List<boqPackageList> boqPackageList(int packId, byte byboq);

        bool TestSendMail();
    }
}
