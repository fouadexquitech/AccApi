using Microsoft.AspNetCore.Mvc;
using AccApi.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using AccApi.Repository.View_Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using AccApi.Repository.View_Models.Request;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierPackagesController : ControllerBase
    {
        private readonly ILogger<SupplierPackagesController> _logger;
        private ISupplierPackagesRepository _supplierPackagesRepository;

        public SupplierPackagesController(ILogger<SupplierPackagesController> logger,ISupplierPackagesRepository supplierPackagesRepository)
        {
            _logger = logger;
            _supplierPackagesRepository = supplierPackagesRepository;
        }

        [HttpGet("GetSupplierPackagesList")]
        public List<SupplierPackagesList> GetSupplierPackagesList(int packageid)
        {
            try
            {
                return this._supplierPackagesRepository.SupplierPackagesList(packageid);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex.Message);
                return null;
            }
        }

       [HttpPost("ValidateExcelBeforeAssign")]
       public string ValidateExcelBeforeAssign(int packId)
        {
            try
            {
                return this._supplierPackagesRepository.ValidateExcelBeforeAssign(packId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AssignPackageSuppliers")]
        public bool AssignPackageSuppliers(int packId,List<SupplierInput> supList, string FilePath)
        {
            try
            {
                return this._supplierPackagesRepository.AssignPackageSuppliers(packId, supList, FilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }
}
