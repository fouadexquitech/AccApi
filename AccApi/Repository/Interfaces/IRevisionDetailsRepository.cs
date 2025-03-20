using AccApi.Data_Layer;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
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
        List<LevelModel> GetRevisionDetails(int RevisionId, string itemDesc, string resource, string CostConn);
        bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate, double discount, byte addedItem, string CostConn);
        bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList, string CostConn);
        bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList,bool isPercent, string CostConn);
        bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList);
        bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList);
        bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList, bool isPercent, string CostConn);
        bool AssignSupplierGroup(int packId, bool byBoq, List<SupplierGroups> SupplierGroupList, bool isPercent, string CostConn);
        bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item, bool isPercent, string CostConn);
        bool AssignSupplierListGroupList(int packId, bool byBoq, AssignSupplierGroup item, bool isPercent, string CostConn);
        bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item, bool isPercent, string CostConn);
        bool SendCompToManagement(TopManagementTemplateModel topManagementTemplate, List<IFormFile> attachements, string UserName, string CostConn);
        List<GroupingLevelModel> GetComparisonSheet(int packageId, SearchInput input, int supId, string CostConn);
        List<GroupingLevelModel> GetComparisonSheetByBoq(int packageId, SearchInput input,int supId, string CostConn);
        List<GroupingBoqGroupModel> GetComparisonSheetResourcesByGroup(int packageId, SearchInput input, string CostConn);
        List<GroupingBoqGroupModel> GetComparisonSheetBoqByGroup(int packageId, SearchInput input, string CostConn);
        
        string GetComparisonSheetByBoq_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst, string CostConn);
        string GetComparisonSheet_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst, string CostConn);
        string GetComparisonSheetBoqByGroup_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst, string CostConn);
        string GetComparisonSheetResourcesByGroup_Excel(int packageId, SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst, string CostConn);
        List<string> GenerateSuppliersContracts_Excel(int packageId,SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst, string CostConn);

        List<AcceptComment> GetRevisionAcceptance(int revId, string CostConn);
        bool ExcludBoq(int packId, string Item,bool isNewItem, bool exclud, string CostConn);
        bool ExcludRessource(int packId, int boqSeq, bool isNewItem, bool isAlternative, bool isExclud, string CostConn);
    }
}
