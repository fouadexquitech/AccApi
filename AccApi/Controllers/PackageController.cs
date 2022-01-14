
using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly ILogger<PackageController> _logger;
        private IPackageRepository _packageRepository;

        public PackageController(ILogger<PackageController> logger, IPackageRepository packageRepository)
        {
            _logger = logger;
            _packageRepository = packageRepository;
        }

        [HttpPost("GetOriginalBoqList")]
        public List<OriginalBoqModel> GetOriginalBoqList(SearchInput input)
        {
            try
            {
                return this._packageRepository.GetOriginalBoqList(input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetBoqList")]
        public List<BoqModel> GetBoqList(string ItemO, SearchInput input)
        {
            try
            {
                return this._packageRepository.GetBoqList(ItemO ,input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        [HttpGet("GetPackageById")]
        public PackageDetailsModel GetPackageById(int IdPkge)
        {
            try
            {
                return this._packageRepository.GetPackageById(IdPkge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AssignPackages")]
        public bool AssignPackages(AssignPackages input)
        {
            try
            {
                return this._packageRepository.AssignPackages(input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet("GetPackageSuppliersPrice")]
        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge)
        {
            try
            {
                return this._packageRepository.GetPackageSuppliersPrice(IdPkge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
