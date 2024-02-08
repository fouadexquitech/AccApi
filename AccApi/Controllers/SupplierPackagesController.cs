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
        public SupplierPackagesList GetSupplierPackage(int psId)
        {
            try
            {
                return this._supplierPackagesRepository.GetSupplierPackage(psId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


        [HttpGet("GetSupplierPackagesList")]
        public List<SupplierPackagesList> GetSupplierPackagesList(int packageid)
        {
            try
            {
                return this._supplierPackagesRepository.GetSupplierPackagesList(packageid);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex.Message);
                return null;
            }
        }

       [HttpPost("ValidateExcelBeforeAssign")]
       public JsonResult ValidateExcelBeforeAssign(int packId, byte byBoq)
        {
            try
            {
                var res=(this._supplierPackagesRepository.ValidateExcelBeforeAssign(packId, byBoq));
                return new JsonResult(res);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
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
        public async Task<bool> AssignPackageSuppliers()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var assignPackageTemplateStr = formCollection["assignPackageTemplate"];
                var assignPackageTemplate = JsonConvert.DeserializeObject<AssignPackageTemplateModel>(assignPackageTemplateStr[0]);

                List<IFormFile> FileAttachments = formCollection.Files.ToList();
                return await this._supplierPackagesRepository.AssignPackageSuppliers(assignPackageTemplate.packId, assignPackageTemplate.supInputList, assignPackageTemplate.ByBoq, assignPackageTemplate.UserName, FileAttachments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
