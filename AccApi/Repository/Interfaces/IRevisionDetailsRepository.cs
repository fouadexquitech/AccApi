using AccApi.Repository.View_Models;
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
        List<RevisionDetailsList> GetRevisionDetails(int RevisionId);
        bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile);
        bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList);
        bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList);
    }
}
