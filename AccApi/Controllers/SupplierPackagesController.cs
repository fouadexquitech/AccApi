using Microsoft.AspNetCore.Mvc;
using AccApi.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using AccApi.Repository.View_Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;

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

        [HttpGet("GetSupplierPackagesList")]
        public List<SupplierPackagesList> GetSupplierPackagesList(int packageid)
        {
            try
            {
                return this._supplierPackagesRepository.SupplierPackagesList(packageid);
            }
            catch (Exception ex)
            {
                _logger.LogError (ex.Message);
                return null;
            }
        }

       [HttpPost("ValidateExcelBeforeAssign")]
       public JsonResult ValidateExcelBeforeAssign(int packId)
        {
            try
            {
                return new JsonResult(this._supplierPackagesRepository.ValidateExcelBeforeAssign(packId));
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

        public FileResult GetFile(string filename)
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


        [HttpGet("DownloadFile")]
        public FileResult DownloadFile(string filename)
        {
            return GetFile(filename);
        }


        [HttpPost("AssignPackageSuppliers")]
        public bool AssignPackageSuppliers(int packId,List<SupplierInput> supList, string FilePath)
        {
            try
            {
                return this._supplierPackagesRepository.AssignPackageSuppliers(packId, supList, FilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }
}
