using AccApi.Repository.Models.MasterModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface IConditionsRepository
    {
        List<TblComCond> GetComConditions();
        List<TblTechCond> GetTechConditions(int packId);
        bool SendTechnicalConditions(int packId);
        bool UpdateCommercialConditions(int PackageSupliersID, IFormFile ExcelFile);

        bool UpdateTechnicalConditions(int PackageSupliersID, IFormFile ExcelFile);

    }

 
}
