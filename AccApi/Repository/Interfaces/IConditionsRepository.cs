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
        List<ComConditions> GetComConditions(int packSupId, string CostConn);
        List<TechConditions> GetTechConditions(int packId, string? filter, string CostConn);
        bool SendTechnicalConditions(int packId, TechCondModel techCondModel, string UserName, string CostConn);
        bool UpdateCommercialConditions(int PackageSupliersRevisionID, string CostConn, IFormFile ExcelFile);
        bool UpdateTechnicalConditions(int packageId, int PackageSupliersRevisionID, string CostConn, IFormFile ExcelFile);
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
        List<ConditionsReply> GetComCondReplyByRevision(int revisionid, string CostConn);
        List<ConditionsReply> GetTechCondReplyByRevision(int revisionid, string CostConn);
        List<TechConditions> GetTechConditionsByPackage(int packId, int revisionId, string CostConn);
        
    }
}
