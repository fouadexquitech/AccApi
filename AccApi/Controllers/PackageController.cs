
using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using AccApi.Repository.View_Models.Common;
using System.Linq;
using AccApi.Repository.Managers;
using System.IO;

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
            _comparisonGroupRepository = comparisonGroupRepository;
        }

        [HttpPost("GetOriginalBoqList")]
        public async Task<List<BoqRessourcesList>> GetOriginalBoqList(SearchInput input,string costDB)
        {
            try
            {
                return await this._packageRepository.GetOriginalBoqList(input,costDB);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpPost("GetBoqList")]
        public List<BoqModel> GetBoqList(string ItemO, SearchInput input)
        {
            try
            {
                return this._packageRepository.GetBoqList(ItemO, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

    
        [HttpPost("ExportBoqExcel")]
        public async Task<JsonResult>  ExportBoqExcel(SearchInput input, string costDB)
        {
            try
            {
                return new JsonResult(await this._packageRepository.ExportBoqExcel(input,  costDB));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpPost("ExportNotAssigned")]
        public async Task<JsonResult> ExportNotAssigned(string costDB)
        {
            try
            {
                return new JsonResult(await this._packageRepository.ExportNotAssigned(costDB));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpPost("ExportExcelPackagesCost")]
        public async Task<JsonResult> ExportExcelPackagesCost(string costDB,int withBoq, SearchInput input)
        {
            try
            {
                return new JsonResult(await this._packageRepository.ExportExcelPackagesCost(withBoq, costDB, input));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }


        [HttpPost("GetGroupBoqListOnly")]
        public List<GroupingBoqModel> GetGroupBoqListOnly(int packageId, int groupId, SearchInput input)
        {
            try
            {
                return this._comparisonGroupRepository.GetBoqListOnly(packageId, groupId, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("AttachToGroupByBoq")]
        public bool AttachToGroupByBoq(int groupId, List<GroupingBoqModel> list)
        {
            try
            {
                return this._comparisonGroupRepository.AttachToGroupByBoq(groupId, list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("DetachFromGroupByBoq")]
        public bool DetachFromGroupByBoq(int groupId, List<GroupingBoqModel> list)
        {
            try
            {
                return this._comparisonGroupRepository.DetachFromGroupByBoq(groupId, list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("updateOriginalBoqQty")]
        public bool updateOriginalBoqQty(OriginalBoqModel boq)
        {
            try
            {
                return this._packageRepository.updateOriginalBoqQty(boq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("updateBoqResQty")]
        public bool updateBoqResQty(BoqModel res)
        {
            try
            {
                return this._packageRepository.updateBoqResQty(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("updateBoqTradeDesc")]
        public bool updateBoqTradeDesc(string tradeDesc, List<OriginalBoqModel> origBoqList)
        {
            try
            {
                return this._packageRepository.updateBoqTradeDesc(tradeDesc, origBoqList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }


        #region Packages 
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
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
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("GetPackageSuppliersPrice")]
        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int IdPkge, SearchInput input)
        {
            try
            {
                return this._packageRepository.GetPackageSuppliersPrice(IdPkge, input);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }


        [HttpPost("GetPackages")]
        public IActionResult GetPackages(dynamic dataTablesParameters)
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
                var request = new DataTablesRequest
                {
                    Length = length,
                    SearchVal = searchVal,
                    SortCol = sortCol,
                    SortDirVal = sortColDir,
                    Start = start
                };

                var response = _packageRepository.GetPackages(request);
                response.Draw = draw;
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpPost("AddPackage")]
        public bool AddPackage(List<Package> packs)
        {
            try
            {
                return this._packageRepository.AddPackage(packs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("UpdatePackage")]
        public bool UpdatePackage(Package pack)
        {
            try
            {
                return this._packageRepository.UpdatePackage(pack);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

        [HttpPost("DeletePackage")]
        public bool DeletePackage(int id)
        {
            try
            {
                return this._packageRepository.DeletePackage(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }
        #endregion



        [HttpPost]
        [Route("GetBoqResourceRecords")]
        public async Task<IActionResult> GetBoqResourceRecords(dynamic dataTablesParameters)
        {
            try
            {
                JObject rest = JsonConvert.DeserializeObject(Convert.ToString(dataTablesParameters));
                int draw = (int)rest["draw"];
                int start = (int)rest["start"];
                int length = (int)rest["length"];
                //int colIndex = (int)rest["order"][0]["column"];
                //string sortCol = (string)rest["columns"][colIndex]["name"];
                //string sortColDir = (string)rest["order"][0]["dir"];
                string searchVal = (string)rest["search"]["value"];
                string? boqIds = (string)rest["boqIds"];
                string? selectedBoqIds = (string)rest["selectedBoqIds"];

                var request = new DataTablesRequest { 
                    BoqItems = boqIds.Split(",").ToList(),
                    Length = length,
                    SearchVal = searchVal,
                    SelectedBoqItems = selectedBoqIds.Split(",").ToList(),
                    //SortCol = sortCol,
                    //SortDirVal = sortColDir,
                    Start = start
                };

                var response = await _packageRepository.GetBoqResourceRecords(request);
                response.Draw = draw;
                return Ok(response);

               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return Ok("false");
            }

        }

    }
}
