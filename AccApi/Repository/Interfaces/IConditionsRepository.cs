using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface IConditionsRepository
    {
        List<ComConditions> GetComConditions();
        List<TechConditions> GetTechConditions(int packId);
        bool SendTechnicalConditions(int packId);
        bool UpdateCommercialConditions(int PackageSupliersID, IFormFile ExcelFile);
        bool UpdateTechnicalConditions(int packageId, int PackageSupliersID, IFormFile ExcelFile);
        List<TmpConditionsReply> GetComConditionsReply(int PackageSupliersID);
        List<TmpConditionsReply> GetTechConditionsReply(int PackageSupliersID);
    }
}
