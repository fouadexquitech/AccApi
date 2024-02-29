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
        List<ComConditions> GetComConditions(int packSupId);
        List<TechConditions> GetTechConditions(int packId, string? filter);
        bool SendTechnicalConditions(int packId, TechCondModel techCondModel, string UserName);
        bool UpdateCommercialConditions(int PackageSupliersRevisionID, IFormFile ExcelFile);
        bool UpdateTechnicalConditions(int packageId, int PackageSupliersRevisionID, IFormFile ExcelFile);
        List<TmpComparisonConditionsReply> GetComConditionsReply(int PackageSupliersID, string costDB, int packageId);
        List<TmpComparisonConditionsReply> GetTechConditionsReply(int PackageSupliersID, string costDB , int packageId);
        List<TmpConditionsReply> GetPackageComConditionsReply(int PackageID);
 
        List<TmpConditionsReply> GetPackageTechConditionsReply(int PackageID);
        bool AddComConditions(List<ComConditions> comcond);
        bool UpdateComConditions(ComConditions comcond);
        bool DelComConditions(int id);
        bool AddTechConditions(TechConditions techcond);
        bool UpdateTechConditions(TechConditions techcond);
        bool DelTechConditions( int id);
        List<ConditionsReply> GetComCondReplyByRevision(int revisionid);
        List<ConditionsReply> GetTechCondReplyByRevision(int revisionid);
        List<TechConditions> GetTechConditionsByPackage(int packId, int revisionId);
        
    }
}
