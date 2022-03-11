﻿using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface IRevisionDetailsRepository
    {
        List<RevisionDetailsList> GetRevisionDetails(int RevisionId, string itemDesc, string resource);
        bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate);
        bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList);
        bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList);
        bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList);
        bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList);
        bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList);
        bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item);
        bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item);
        bool SendCompToManagement(int packId, List<TopManagement> topManagList, IFormFile ExcelComparisonSheet);
    }
}
