﻿using AccApi.Data_Layer;
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
        List<LevelModel> GetRevisionDetails(int RevisionId, string itemDesc, string resource);
        bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate, double discount, byte addedItem);
        bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList);
        bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList,bool isPercent);
        bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList);
        bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList);
        bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList, bool isPercent);
        bool AssignSupplierGroup(int packId, bool byBoq, List<SupplierGroups> SupplierGroupList, bool isPercent);
        bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item, bool isPercent);
        bool AssignSupplierListGroupList(int packId, bool byBoq, AssignSupplierGroup item, bool isPercent);
        bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item, bool isPercent);
        bool SendCompToManagement(TopManagementTemplateModel topManagementTemplate, List<IFormFile> attachements, string UserName);
        List<GroupingLevelModel> GetComparisonSheet(int packageId, SearchInput input, int supId);
        List<GroupingLevelModel> GetComparisonSheetByBoq(int packageId, SearchInput input,int supId);
        List<GroupingBoqGroupModel> GetComparisonSheetResourcesByGroup(int packageId, SearchInput input);
        List<GroupingBoqGroupModel> GetComparisonSheetBoqByGroup(int packageId, SearchInput input);
        
        string GetComparisonSheetByBoq_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst);
        string GetComparisonSheet_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst);
        string GetComparisonSheetBoqByGroup_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst);
        string GetComparisonSheetResourcesByGroup_Excel(int packageId, SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst);
        List<string> GenerateSuppliersContracts_Excel(int packageId,SearchInput input, List<TmpComparisonConditionsReply> comcondRepLst, List<TmpComparisonConditionsReply> techcondRepLst);

        List<AcceptComment> GetRevisionAcceptance(int revId);
        bool ExcludBoq(int packId, string Item,bool isNewItem, bool exclud);
        bool ExcludRessource(int packId, int boqSeq, bool isNewItem, bool isAlternative, bool isExclud);
    }
}
