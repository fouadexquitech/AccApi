using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private ISearchRepository _searchRepository;
        public SearchController(ILogger<SearchController> logger, ISearchRepository searchRepository)
        {
            _logger = logger;
            _searchRepository = searchRepository;
        }

        [HttpGet("GetBOQDivList")]
        public List<BOQDivList> GetBOQDivList()
        {
            try
            {
                return this._searchRepository.GetBOQDivList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetBOQLevel2List")]
        public List<BOQLevelList> GetBOQLevel2List()
        {
            try
            {
                return this._searchRepository.GetBOQLevel2List();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        [HttpGet("GetBOQLevel3List")]
        public List<BOQLevelList> GetBOQLevel3List()
        {
            try
            {
                return this._searchRepository.GetBOQLevel3List();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetBOQLevel4List")]
        public List<BOQLevelList> GetBOQLevel4List()
        {
            try
            {
                return this._searchRepository.GetBOQLevel4List();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        [HttpPost("GetBOQLevel3ListByLevel2")]
        public List<BOQLevelList> GetBOQLevel3ListByLevel2(RessourceLevelsFilter filter)
        {
            try
            {
                return this._searchRepository.GetBOQLevel3ListByLevel2(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetBOQLevel4ListByLevel3")]
        public List<BOQLevelList> GetBOQLevel4ListByLevel3(RessourceLevelsFilter filter)
        {
            try
            {
                return this._searchRepository.GetBOQLevel4ListByLevel3(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetRESDivList")]
        public List<RESDivList> GetRESDivList()
        {
            try
            {
                return this._searchRepository.RESDivList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetRESTypeList")]
        public List<RESTypeList> GetRESTypeList()
        {
            try
            {
                return this._searchRepository.RESTypeList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("PackageList")]
        public List<Package> GetPackageList()
        {
            try
            {
                return this._searchRepository.PackageList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetRESPackageList")]
        public List<RESPackageList> GetRESPackageList()
        {
            try
            {
                return this._searchRepository.RESPackageList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetSheetDescList")]
        public List<SheetDescList> GetSheetDescList()
        {
            try
            {
                return this._searchRepository.SheetDescList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        [HttpGet("GetRessourcesList")]
        public List<RessourceList> GetRessourcesList()
        {
            try
            {
                return this._searchRepository.GetRessourcesList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetRessourcesListByLevels")]
        public List<RessourceList> GetRessourcesListByLevels(RessourceLevelsFilter filter)
        {
            try
            {
                return this._searchRepository.GetRessourcesListByLevels(filter);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}
