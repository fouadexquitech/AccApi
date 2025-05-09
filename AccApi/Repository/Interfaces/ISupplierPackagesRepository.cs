﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Http;

namespace AccApi.Repository.Interfaces
{
    public interface ISupplierPackagesRepository 
    {
        List<SupplierPackagesList> GetSupplierPackagesList(int packageid, string CostConn);

        SupplierPackagesList GetSupplierPackage(int spId, string CostConn);

        string ValidateExcelBeforeAssign(int packId, byte byBoq, bool withPrice, string CostConn);
        Task<bool> AssignPackageSuppliers(int packId,List<SupplierInputList> supInputList, byte ByBoq, string UserName, List<IFormFile> attachments,DateTime RevExpiryDate, string CostConn);
        List<boqPackageList> GetboqPackageList(int packId, byte byboq, string CostConn);

        bool TestSendMail();
    }
}
