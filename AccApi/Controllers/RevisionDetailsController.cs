using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisionDetailsController : ControllerBase
    {
       private readonly ILogger<RevisionDetailsController> _logger;
       private readonly IRevisionDetailsRepository _revisionDetailsRepository;
       private IConditionsRepository _conditionsRepository;
       private ISupplierPackagesRepository _supplierPackagesRepository;    

        public RevisionDetailsController (ILogger<RevisionDetailsController> logger, IRevisionDetailsRepository revisionDetailsRepository, IConditionsRepository conditionsRepository, ISupplierPackagesRepository supplierPackagesRepository)
        {
            _logger = logger;
            _revisionDetailsRepository=revisionDetailsRepository;
            _conditionsRepository = conditionsRepository;
            _supplierPackagesRepository = supplierPackagesRepository;
        }

        [HttpGet("GetRevisionDetails")]
        public List<RevisionDetailsList> GetRevisionDetails(int RevisionId, string itemDesc, string resource)
        {
            try
            {
                return this._revisionDetailsRepository.GetRevisionDetails(RevisionId, itemDesc, resource);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AddRevision")]
        public bool AddRevision(int PackageSupplierId, string PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate)
        {
            try
            {
                return this._revisionDetailsRepository.AddRevision(PackageSupplierId, Convert.ToDateTime(PackSuppDate),  ExcelFile, curId,  ExchRate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierPackage")]
        public bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierPackage(packId, SupPercentList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateRevisionDetailsPrice")]
        public bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList)
        {
            try
            {
                return this._revisionDetailsRepository.UpdateRevisionDetailsPrice(revisionDetailsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateRevisionDetailsPriceByBoq")]
        public bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList)
        {
            try
            {
                return this._revisionDetailsRepository.UpdateRevisionDetailsPriceByBoq(revisionDetailsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierRessource")]
        public bool AssignSupplierRessource(int packId, bool isPercent, List<SupplierResrouces> supplierResList)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierRessource(packId, supplierResList, isPercent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierBOQ")]
        public bool AssignSupplierBOQ(int packId, bool isPercent, List<SupplierBOQ> SupplierBOQList)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierBOQ(packId, SupplierBOQList,  isPercent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierListBoqList")]
        public bool AssignSupplierListBoqList(int packId, bool isPercent, AssignSuppliertBoq item)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListBoqList(packId, item,  isPercent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierListRessourceList")]
        public bool AssignSupplierListRessourceList(int packId, bool isPercent, AssignSuppliertRes item)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListRessourceList(packId, item,  isPercent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("SendCompToManagement")]
        public async Task<bool> SendCompToManagement()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var topManagementTemplateStr = formCollection["topManagementTemplate"];
                var topManagementTemplate = JsonConvert.DeserializeObject<TopManagementTemplateModel>(topManagementTemplateStr[0]);

                var attachement = formCollection.Files[0];

                return this._revisionDetailsRepository.SendCompToManagement(topManagementTemplate, attachement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        [HttpPost("GetComparisonSheet")]
        public List<GroupingBoqModel> GetComparisonSheet(int packageId, SearchInput input)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheet(packageId, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheetByBoq")]
        public List<GroupingBoqModel> GetComparisonSheetByBoq(int packageId, SearchInput input)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheetByBoq(packageId, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheetByBoq_Excel")]
        public string GetComparisonSheetByBoq_Excel(int packageId, SearchInput input)
        {
            try
            {
                List<boqPackageList> boqPackageList = this._supplierPackagesRepository.boqPackageList(packageId, 1);
                List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                return this._revisionDetailsRepository.GetComparisonSheetByBoq_Excel(packageId, input, boqPackageList, comcondRepLst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheet_Excel")]
        public string GetComparisonSheet_Excel(int packageId, SearchInput input)
        {
            try
            {
                List<boqPackageList> boqPackageList = this._supplierPackagesRepository.boqPackageList(packageId, 0);
                List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                return this._revisionDetailsRepository.GetComparisonSheet_Excel(packageId, input, boqPackageList, comcondRepLst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheetResourcesByGroup")]
        public List<GroupingBoqGroupModel> GetComparisonSheetResourcesByGroup(int packageId, SearchInput input)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheetResourcesByGroup(packageId, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheetResourcesByGroup_Excel")]
        public string GetComparisonSheetResourcesByGroup_Excel(int packageId, SearchInput input)
        {
            try
            {
                List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                return this._revisionDetailsRepository.GetComparisonSheetResourcesByGroup_Excel(packageId, input, comcondRepLst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheetBoqByGroup")]
        public List<GroupingBoqGroupModel> GetComparisonSheetBoqByGroup(int packageId, SearchInput input)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheetBoqByGroup(packageId, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
     
        [HttpPost("GetComparisonSheetBoqByGroup_Excel")]
        public string GetComparisonSheetBoqByGroup_Excel(int packageId, SearchInput input)
        {
            try
            {
                List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                return this._revisionDetailsRepository.GetComparisonSheetBoqByGroup_Excel(packageId, input, comcondRepLst);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
      
        [HttpPost("AssignSupplierListGroupList")]
        public bool AssignSupplierListGroupList(int packId, bool byBoq, bool isPercent, AssignSupplierGroup item)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListGroupList(packId, byBoq, item,  isPercent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
      
        [HttpPost("AssignSupplierGroup")]
        public bool AssignSupplierGroup(int packId, bool byBoq, bool isPercent, List<SupplierGroups> SupplierGroupList)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierGroup(packId, byBoq, SupplierGroupList,  isPercent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
