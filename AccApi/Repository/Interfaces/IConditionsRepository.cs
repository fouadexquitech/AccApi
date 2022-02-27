using AccApi.Repository.Models.MasterModels;
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

    }

 
}
