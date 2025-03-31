
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
       private IConditionsRepository _conditionsRepository;
       private ISupplierPackagesRepository _supplierPackagesRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RevisionDetailsController (ILogger<RevisionDetailsController> logger, 
            IRevisionDetailsRepository revisionDetailsRepository, 
            IConditionsRepository conditionsRepository, 
            ISupplierPackagesRepository supplierPackagesRepository,
            IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _revisionDetailsRepository=revisionDetailsRepository;
            _conditionsRepository = conditionsRepository;
            _supplierPackagesRepository = supplierPackagesRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("GetRevisionDetails")]
        public List<LevelModel> GetRevisionDetails(int RevisionId, string itemDesc, string resource, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.GetRevisionDetails(RevisionId, itemDesc, resource, CostConn);
            }
            catch (Exception ex)
            {
                 _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("AddRevision")]
        public bool AddRevision(int PackageSupplierId, string PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate, double discount, byte addedItem, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AddRevision(PackageSupplierId, Convert.ToDateTime(PackSuppDate),  ExcelFile, curId,  ExchRate, discount, addedItem,CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("AssignSupplierPackage")]
        public bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierPackage(packId, SupPercentList, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("UpdateRevisionDetailsPrice")]
        public bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList)
        {
            try
            {
                return this._revisionDetailsRepository.UpdateRevisionDetailsPrice(revisionDetailsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("UpdateRevisionDetailsPriceByBoq")]
        public bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList)
        {
            try
            {
                return this._revisionDetailsRepository.UpdateRevisionDetailsPriceByBoq(revisionDetailsList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("AssignSupplierRessource")]
        public bool AssignSupplierRessource(int packId, bool isPercent, List<SupplierResrouces> supplierResList, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierRessource(packId, supplierResList, isPercent, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("AssignSupplierBOQ")]
        public bool AssignSupplierBOQ(int packId, bool isPercent, List<SupplierBOQ> SupplierBOQList, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierBOQ(packId, SupplierBOQList,  isPercent,  CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("AssignSupplierListBoqList")]
        public bool AssignSupplierListBoqList(int packId, bool isPercent, AssignSuppliertBoq item, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListBoqList(packId, item,  isPercent,  CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("AssignSupplierListRessourceList")]
        public bool AssignSupplierListRessourceList(int packId, bool isPercent, AssignSuppliertRes item, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListRessourceList(packId, item,  isPercent,  CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("SendCompToManagement")]
        public async Task<bool> SendCompToManagement(string CostConn)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var topManagementTemplateStr = formCollection["topManagementTemplate"];
                var topManagementTemplate = JsonConvert.DeserializeObject<TopManagementTemplateModel>(topManagementTemplateStr[0]);

                List<IFormFile> attachements = formCollection.Files.ToList();

                return this._revisionDetailsRepository.SendCompToManagement(topManagementTemplate, attachements, topManagementTemplate.UserName,  CostConn);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }


        [HttpPost("GetComparisonSheet")]
        public List<GroupingLevelModel> GetComparisonSheet(int packageId, SearchInput input, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheet(packageId, input,0, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetComparisonSheetByBoq")]
        public List<GroupingLevelModel> GetComparisonSheetByBoq(int packageId, SearchInput input, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheetByBoq(packageId, input,0, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetComparisonSheetByBoq_Excel")]
        public JsonResult GetComparisonSheetByBoq_Excel(int packageId, SearchInput input, int PackageSupliersID, string costDB, string CostConn)
        {
            try
            {
                List<boqPackageList> boqPackageList = this._supplierPackagesRepository.boqPackageList(packageId, 1, CostConn);
                //AH27022024
                //List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                //List<TmpConditionsReply> techcondRepLst = this._conditionsRepository.GetPackageTechConditionsReply(packageId);
                List<TmpComparisonConditionsReply> comcondRepLst = this._conditionsRepository.GetComConditionsReply(PackageSupliersID, costDB, packageId);
                List<TmpComparisonConditionsReply> techcondRepLst = this._conditionsRepository.GetTechConditionsReply(PackageSupliersID, costDB, packageId);
                //AH27022024

                return new JsonResult(this._revisionDetailsRepository.GetComparisonSheetByBoq_Excel(packageId, input, boqPackageList, comcondRepLst, techcondRepLst, CostConn));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetComparisonSheet_Excel")]
        public JsonResult GetComparisonSheet_Excel(int packageId, SearchInput input, int PackageSupliersID, string costDB,string CostConn)
        {
            try
            {
                List<boqPackageList> boqPackageList = this._supplierPackagesRepository.boqPackageList(packageId, 0, CostConn);
                //AH27022024
                //List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                //List<TmpConditionsReply> techcondRepLst = this._conditionsRepository.GetPackageTechConditionsReply(packageId);
                List<TmpComparisonConditionsReply> comcondRepLst = this._conditionsRepository.GetComConditionsReply(PackageSupliersID, costDB, packageId);
                List<TmpComparisonConditionsReply> techcondRepLst = this._conditionsRepository.GetTechConditionsReply(PackageSupliersID, costDB, packageId);
                //AH27022024
                return new JsonResult(this._revisionDetailsRepository.GetComparisonSheet_Excel(packageId, input, boqPackageList, comcondRepLst, techcondRepLst, CostConn));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetComparisonSheetResourcesByGroup")]
        public List<GroupingBoqGroupModel> GetComparisonSheetResourcesByGroup(int packageId, SearchInput input, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheetResourcesByGroup(packageId, input,  CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetComparisonSheetResourcesByGroup_Excel")]
        public JsonResult GetComparisonSheetResourcesByGroup_Excel(int packageId, SearchInput input, int PackageSupliersID, string costDB, string CostConn)
        {
            try
            {
                //AH27022024
                //List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                //List<TmpConditionsReply> techcondRepLst = this._conditionsRepository.GetPackageTechConditionsReply(packageId);
                List<TmpComparisonConditionsReply> comcondRepLst = this._conditionsRepository.GetComConditionsReply(PackageSupliersID, costDB, packageId);
                List<TmpComparisonConditionsReply> techcondRepLst = this._conditionsRepository.GetTechConditionsReply(PackageSupliersID, costDB, packageId);
                //AH27022024
                return new JsonResult(this._revisionDetailsRepository.GetComparisonSheetResourcesByGroup_Excel(packageId, input, comcondRepLst, techcondRepLst, CostConn));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetComparisonSheetBoqByGroup")]
        public List<GroupingBoqGroupModel> GetComparisonSheetBoqByGroup(int packageId, SearchInput input, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.GetComparisonSheetBoqByGroup(packageId, input, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("GetComparisonSheetBoqByGroup_Excel")]
        public JsonResult GetComparisonSheetBoqByGroup_Excel(int packageId, SearchInput input, int PackageSupliersID, string costDB, string CostConn)
        {
            try
            {
                //AH27022024
                //List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                //List<TmpConditionsReply> techcondRepLst = this._conditionsRepository.GetPackageTechConditionsReply(packageId);
                List<TmpComparisonConditionsReply> comcondRepLst = this._conditionsRepository.GetComConditionsReply(PackageSupliersID, costDB, packageId);
                List<TmpComparisonConditionsReply> techcondRepLst = this._conditionsRepository.GetTechConditionsReply(PackageSupliersID, costDB, packageId);
                //AH27022024
                List<boqPackageList> boqPackageList = this._supplierPackagesRepository.boqPackageList(packageId, 0, CostConn);
                return new JsonResult(this._revisionDetailsRepository.GetComparisonSheetBoqByGroup_Excel(packageId, input, boqPackageList, comcondRepLst, techcondRepLst, CostConn));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }
      


        [HttpPost("AssignSupplierListGroupList")]
        public bool AssignSupplierListGroupList(int packId, bool byBoq, bool isPercent, AssignSupplierGroup item, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierListGroupList(packId, byBoq, item,  isPercent, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }
      
        [HttpPost("AssignSupplierGroup")]
        public bool AssignSupplierGroup(int packId, bool byBoq, bool isPercent, List<SupplierGroups> SupplierGroupList, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.AssignSupplierGroup(packId, byBoq, SupplierGroupList,  isPercent, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }


        [HttpPost("GenerateSuppliersContracts_Excel")]
        public JsonResult GenerateSuppliersContracts_Excel(int packageId, SearchInput input, int PackageSupliersID, string costDB, string CostConn)
        {
            try
            {
                //AH27022024
                //List<TmpConditionsReply> comcondRepLst = this._conditionsRepository.GetPackageComConditionsReply(packageId);
                //List<TmpConditionsReply> techcondRepLst = this._conditionsRepository.GetPackageTechConditionsReply(packageId);
                List<TmpComparisonConditionsReply> comcondRepLst = this._conditionsRepository.GetComConditionsReply(PackageSupliersID, costDB, packageId);
                List<TmpComparisonConditionsReply> techcondRepLst = this._conditionsRepository.GetTechConditionsReply(PackageSupliersID, costDB, packageId);
                //AH27022024

                return new JsonResult(this._revisionDetailsRepository.GenerateSuppliersContracts_Excel(packageId,input,comcondRepLst,techcondRepLst,  CostConn));
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }
        private string GetMimeType(string fileName)
        {
            // Make Sure Microsoft.AspNetCore.StaticFiles Nuget Package is installed
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }


        [HttpGet("DownloadFile")]
        public FileResult DownloadFile(string filename)
        {
            var filepath = Path.Combine($"{this._hostingEnvironment.ContentRootPath}\\{filename}");

            var mimeType = this.GetMimeType(filename);

            byte[] fileBytes;

            if (System.IO.File.Exists(filepath))
            {
                fileBytes = System.IO.File.ReadAllBytes(filepath);
            }
            else
            {
                return null;
            }

            return File(fileBytes, "application/octet-stream", filename);
        }

        //[HttpGet("GetExchangeRate")]
        //public string GetExchangeRate(string fromCurrency, string toCurrency)
        //{
        //    try
        //    { 
        //    const string urlPattern = "https://free.currconv.com/api/v7/convert?q={0}_{1}&compact=ultra&apiKey=7726dd1cebe5aeb063da";
        //    string url = string.Format(urlPattern, fromCurrency, toCurrency);

        //        using (var wc = new WebClient())
        //        {
        //            var json = wc.DownloadString(url);
        //            Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(json);
        //            string exchangeRate = (string)token.SelectToken("USD_AED[0]");
        //            return exchangeRate;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return "";
        //    }
        //}


        [HttpPost("GetRevisionAcceptance")]
        public List<AcceptComment> GetRevisionAcceptance(int revId, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.GetRevisionAcceptance(revId,  CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return null;
            }
        }

        [HttpPost("ExcludBoq")]
        public bool ExcludBoq(int packId, string Item, bool isNewItem, bool isExclud, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.ExcludBoq(packId, Item, isNewItem,isExclud,  CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }

        [HttpPost("ExcludRessource")]
        public bool ExcludRessource(int packId, int boqSeq, bool isNewItem, bool isAlternative, bool isExclud, string CostConn)
        {
            try
            {
                return this._revisionDetailsRepository.ExcludRessource(packId, boqSeq, isNewItem, isAlternative, isExclud, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                return false;
            }
        }
    }
}
