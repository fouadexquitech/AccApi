using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public RevisionDetailsController (ILogger<RevisionDetailsController> logger, IRevisionDetailsRepository revisionDetailsRepository)
        {
            _logger = logger;
            _revisionDetailsRepository=revisionDetailsRepository;
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
        public bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierRessource(packId, supplierResList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierBOQ")]
        public bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierBOQ(packId, SupplierBOQList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierListBoqList")]
        public bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListBoqList(packId, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("AssignSupplierListRessourceList")]
        public bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListRessourceList(packId, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("SendCompToManagement")]
        public bool SendCompToManagement(int packId, List<TopManagement> topManagList)
        {
            try
            {
                return this._revisionDetailsRepository.SendCompToManagement(packId, topManagList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
