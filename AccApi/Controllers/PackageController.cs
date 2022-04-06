
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
        private IComparisonGroupRepository _comparisonGroupRepository;

        public PackageController(ILogger<PackageController> logger, IPackageRepository packageRepository, IComparisonGroupRepository comparisonGroupRepository)
        {
            _logger = logger;
            _packageRepository = packageRepository;
            _comparisonGroupRepository = comparisonGroupRepository;
        }

        [HttpPost("GetOriginalBoqList")]
        public List<BoqRessourcesList> GetOriginalBoqList(SearchInput input)
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

        [HttpPost("GetAllBoqList")]
        public List<BoqModel> GetAllBoqList(string ItemO, SearchInput input)
        {
            try
            {
                return this._packageRepository.GetAllBoqList(input);
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

        [HttpPost("GetPackageSuppliersPrice")]
        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge, SearchInput input)
        {
            try
            {
                return this._packageRepository.GetPackageSuppliersPrice(IdPkge,input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetGroupBoqList")]
        public List<GroupingBoqModel> GetGroupBoqList(int packageId, int groupId, SearchInput input)
        {
            try
            {
                return this._comparisonGroupRepository.GetBoqList(packageId, groupId, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AddGroup")]
        public bool AddGroup(ComparisonPackageGroupModel comparisonPackageGroup)
        {
            try
            {
                return this._comparisonGroupRepository.AddGroup(comparisonPackageGroup);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet("GetGroups")]
        public List<ComparisonPackageGroupModel> GetGroups(int packageId)
        {
            try
            {
                return this._comparisonGroupRepository.GetGroups(packageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AttachToGroup")]
        public bool AttachToGroup(int groupId, List<GroupingResourceModel> list)
        {
            try
            {
                return this._comparisonGroupRepository.AttachToGroup(groupId, list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("DetachFromGroup")]
        public bool DetachFromGroup(int groupId, List<GroupingResourceModel> list)
        {
            try
            {
                return this._comparisonGroupRepository.DetachFromGroup(groupId, list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
