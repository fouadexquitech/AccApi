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
        public List<RevisionDetailsList> GetRevisionDetails(int RevisionId)
        {
            try
            {
                return this._revisionDetailsRepository.GetRevisionDetails(RevisionId);
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

    }
}
