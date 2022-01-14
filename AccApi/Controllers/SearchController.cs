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
                return this._searchRepository.BOQDivList();
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
        public List<PackageList> GetPackageList()
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
    }
}
