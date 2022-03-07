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
        bool UpdateTechnicalConditions(int PackageSupliersID, IFormFile ExcelFile);
        List<ConditionsReply> GetComConditionsReply(int PackageSupliersID);
        List<ConditionsReply> GetTechConditionsReply(int PackageSupliersID);
    }
}
