using AccApi.Repository.Interfaces;
using AccApi.Repository.Managers;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public List<Supplier> GetSupplierList(int packID)
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

        [HttpGet("GetSupplierList_NotAssignetPackage")]
        public ActionResult<List<Supplier>> GetSupplierList_NotAssignetPackage([FromQuery] int packID, [FromQuery] int portalStatus, [FromQuery] string CostConn)
        {
            try
            {
                if (packID <= 0)
                {
                    return BadRequest(
                        new
                        {
                            message = "A valid package ID is required."
                        }
                    );
                }

                if (
                    portalStatus != 0 &&
                    portalStatus != 1
                )
                {
                    return BadRequest(
                        new
                        {
                            message = "Portal status must be 0 or 1."
                        }
                    );
                }

                List<Supplier> suppliers = _supplierRepository.GetSupplierList_NotAssignetPackage(packID, portalStatus, CostConn);

                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error loading unassigned suppliers. " +
                    "Package ID: {PackageId}, " +
                    "Portal Status: {PortalStatus}",
                    packID,
                    portalStatus
                );

                return StatusCode(
                    StatusCodes
                        .Status500InternalServerError,
                    new
                    {
                        message =
                            ex.Message
                    }
                );
            }
        }
        //[HttpGet("GetSupplierList_NotAssignetPackage")]
        //public List<Supplier> GetSupplierList_NotAssignetPackage(int packID, string CostConn)
        //{
        //    try
        //    {
        //        return this._supplierRepository.GetSupplierList_NotAssignetPackage(packID,  CostConn);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return null;
        //    }
        //}

        [HttpPost("GetSuppliers")]
        public IActionResult GetSuppliers(dynamic dataTablesParameters)
        {
            try
            {
                JObject rest = JsonConvert.DeserializeObject(Convert.ToString(dataTablesParameters));
                int draw = (int)rest["draw"];
                int start = (int)rest["start"];
                int length = (int)rest["length"];
                int colIndex = (int)rest["order"][0]["column"];
                string sortCol = (string)rest["columns"][colIndex]["name"];
                string sortColDir = (string)rest["order"][0]["dir"];
                string searchVal = (string)rest["search"]["value"];
                var request = new DataTablesRequest { 
                    Length = length,
                    SearchVal = searchVal,
                    SortCol = sortCol,
                    SortDirVal = sortColDir,
                    Start = start
                };

                var response = _supplierRepository.GetSuppliers(request);
                response.Draw = draw;
                return Ok(response);
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AddSupplier")]
        public bool AddSupplier(List<Supplier> sups)
        {
            try
            {
                return this._supplierRepository.AddSupplier(sups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateSupplier")]
        public bool UpdateSupplier(Supplier sup)
        {
            try
            {
                return this._supplierRepository.UpdateSupplier(sup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("DeleteSupplier")]
        public bool DeleteSupplier(int id)
        {
            try
            {
                return this._supplierRepository.DeleteSupplier(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdatePortalAccountFlag")]
        public async Task<bool> UpdatePortalAccountFlag(SupplierPortalAccountFlagViewModel model)
        {
            try
            {
                return await this._supplierRepository.UpdatePortalAccountFlag(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        [HttpPost("Register")]
        public async Task<ResponseModel<bool>> Register(List<RegisterModel> model)
        {
            try
            {
                return await _supplierRepository.Register(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Something went wrong!"
                };

            }
        }

    }
}
