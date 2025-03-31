using Microsoft.AspNetCore.Mvc;
using AccApi.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using AccApi.Repository.View_Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierPackagesController : ControllerBase
    {
        private readonly ILogger<SupplierPackagesController> _logger;
        private ISupplierPackagesRepository _supplierPackagesRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public SupplierPackagesController(ILogger<SupplierPackagesController> logger,ISupplierPackagesRepository supplierPackagesRepository, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _supplierPackagesRepository = supplierPackagesRepository;
            this._hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("GetSupplierPackage")]
        public SupplierPackagesList GetSupplierPackage(int psId, string CostConn)
        {
            try
            {
                return this._supplierPackagesRepository.GetSupplierPackage(psId, CostConn);
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
                //return error;
                return null;
            }
        }


        [HttpGet("GetSupplierPackagesList")]
        public List<SupplierPackagesList> GetSupplierPackagesList(int packageid, string CostConn)
        {
            try
            {
                return this._supplierPackagesRepository.GetSupplierPackagesList(packageid, CostConn);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
                }
                //return error;
                return null;
            }
        }

       [HttpPost("ValidateExcelBeforeAssign")]
       public JsonResult ValidateExcelBeforeAssign(int packId, byte byBoq, string CostConn)
        {
            try
            {
                var res=(this._supplierPackagesRepository.ValidateExcelBeforeAssign(packId, byBoq,true,CostConn));
                return new JsonResult(res);
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

        //public FileResult GetFile(string filename)
        //{
        //    var filepath = Path.Combine($"{this._hostingEnvironment.ContentRootPath}\\{filename}");

        //    var mimeType = this.GetMimeType(filename);

        //    byte[] fileBytes;

        //    if (System.IO.File.Exists(filepath))
        //    {
        //        fileBytes = System.IO.File.ReadAllBytes(filepath);
        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    return File(fileBytes, "application/octet-stream", filename);
        //}


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


        [HttpPost("AssignPackageSuppliers")]
        public async Task<bool> AssignPackageSuppliers(string CostConn)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var assignPackageTemplateStr = formCollection["assignPackageTemplate"];
                var assignPackageTemplate = JsonConvert.DeserializeObject<AssignPackageTemplateModel>(assignPackageTemplateStr[0]);

                List<IFormFile> FileAttachments = formCollection.Files.ToList();
                return await this._supplierPackagesRepository.AssignPackageSuppliers(assignPackageTemplate.packId, assignPackageTemplate.supInputList, assignPackageTemplate.ByBoq, assignPackageTemplate.UserName, FileAttachments, assignPackageTemplate.RevisionExpiryDate, CostConn);
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

        [HttpPost("TestSendMail")]
        public bool TestSendMail()
        {
            try
            {
                this._supplierPackagesRepository.TestSendMail();
                return true;
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
