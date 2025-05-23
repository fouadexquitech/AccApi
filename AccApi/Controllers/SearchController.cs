﻿using AccApi.Repository;
using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private ISearchRepository _searchRepository;
        private GlobalLists _globalLists;

        public SearchController(ILogger<SearchController> logger, ISearchRepository searchRepository, GlobalLists globalLists)
        {
            _logger = logger;
            _searchRepository = searchRepository;
            _globalLists = globalLists;
        }

        [HttpPost("GetBOQDivList")]
        public List<BOQDivList> GetBOQDivList(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetBOQDivList(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }


        [HttpPost("GetBOQLevel2List")]
        public List<BOQLevelList> GetBOQLevel2List(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetBOQLevel2List(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }


        //[HttpPost("GetBOQLevel2ListBy")]
        //public List<BOQLevelList> GetBOQLevel2ListBy(RessourceLevelsFilter filter)
        //{
        //    try
        //    {
        //        return this._searchRepository.GetBOQLevel2ListBy(filter);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return null;
        //    }
        //}

        [HttpPost("GetBOQLevel3List")]
        public List<BOQLevelList> GetBOQLevel3List(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetBOQLevel3List(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetBOQLevel4List")]
        public List<BOQLevelList> GetBOQLevel4List(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetBOQLevel4List(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetBOQLevel3ListByLevel2")]
        public List<BOQLevelList> GetBOQLevel3ListByLevel2(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetBOQLevel3ListByLevel2(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetBOQLevel4ListByLevel3")]
        public List<BOQLevelList> GetBOQLevel4ListByLevel3(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetBOQLevel4ListByLevel3(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetResTypeList")]
        public List<RESTypeList> GetResTypeList(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetResTypeList(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpGet("GetRESDivList")]
        public List<RESDivList> GetRESDivList(string CostConn)
        {
            try
            {
                return this._searchRepository.RESDivList( CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }


        [HttpGet("GetPackagesList")]
        public List<Package> GetPackagesList(bool usedPackages, string CostConn)
        {
            try
            {
                return this._searchRepository.GetPackagesList(usedPackages, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpGet("GetRESPackageList")]
        public List<RESPackageList> GetRESPackageList(string CostConn)
        {
            try
            {
                return this._searchRepository.RESPackageList( CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpGet("GetSheetDescList")]
        public List<SheetDescList> GetSheetDescList(string CostConn)
        {
            try
            {
                return this._searchRepository.SheetDescList(CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }


        [HttpPost("GetRessourcesList")]
        public List<RessourceList> GetRessourcesList(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetRessourcesList(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetRessourcesListByLevels")]
        public List<RessourceList> GetRessourcesListByLevels(RessourceLevelsFilter filter, string CostConn)
        {
            try
            {
                return this._searchRepository.GetRessourcesListByLevels(filter, CostConn);
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

    }
}
