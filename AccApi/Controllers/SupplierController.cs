using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ILogger<SupplierController> _logger;
        private ISupplierRepository _supplierRepository;

        public SupplierController(ILogger<SupplierController> logger, ISupplierRepository supplierRepository)
        {
            _logger = logger;
            _supplierRepository = supplierRepository;
        }

        [HttpGet("GetSupplierList")]
        public List<SupplierList> GetSupplierList(int packID)
        {
            try
            {
                return this._supplierRepository.SupplierList(packID);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
