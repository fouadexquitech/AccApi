using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
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
using System.Text.Json;
using System.Threading.Tasks;

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
       public JsonResult ValidateExcelBeforeAssign(int packId, byte byBoq,bool withPrice, string CostConn)
        {
            try
            {
                var res=(this._supplierPackagesRepository.ValidateExcelBeforeAssign(packId, byBoq, withPrice, CostConn));
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
        public async Task<IActionResult> AssignPackageSuppliers(    [FromQuery] string CostConn,        [FromQuery] string TSConn)
        {
            try
            {
                IFormCollection form =
                    await Request.ReadFormAsync();

                string serializedTemplate =
                    form["assignPackageTemplate"]
                        .FirstOrDefault();

                if (
                    string.IsNullOrWhiteSpace(
                        serializedTemplate
                    )
                )
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message =
                                "Assign package template is required."
                        }
                    );
                }

                AssignPackageTemplateModel assignTemplate =
                    System.Text.Json.JsonSerializer.Deserialize
                    <AssignPackageTemplateModel>(
                        serializedTemplate,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        }
                    );

                if (assignTemplate == null)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message =
                                "Invalid assign package template."
                        }
                    );
                }

                if (
                    assignTemplate.SupInputList == null ||
                    assignTemplate.SupInputList.Count == 0
                )
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message =
                                "At least one supplier is required."
                        }
                    );
                }



                List<string> emailCc =
    NormalizeEmailList(
        assignTemplate.ListCC
    );

                if (
                    assignTemplate.SupInputList == null ||
                    assignTemplate.SupInputList.Count == 0
                )
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message =
                                "At least one supplier is required."
                        }
                    );
                }

                foreach (
                    SupplierInputList supplierInput
                    in assignTemplate.SupInputList
                )
                {
                    supplierInput.mailTo =
                        NormalizeEmailList(
                            supplierInput.mailTo
                        );

                    if (
                        supplierInput.mailTo == null ||
                        supplierInput.mailTo.Count == 0
                    )
                    {
                        return BadRequest(
                            new
                            {
                                success = false,
                                message =
                                    "Email To is required for supplier " +
                                    (
                                        supplierInput.supplierName ??
                                        supplierInput
                                            .supplierInput
                                            ?.supID
                                            .ToString()
                                    )
                            }
                        );
                    }

                    List<string> invalidSupplierEmails =
                        supplierInput.mailTo
                            .Where(email =>
                                !IsValidEmail(email)
                            )
                            .ToList();

                    if (invalidSupplierEmails.Count > 0)
                    {
                        return BadRequest(
                            new
                            {
                                success = false,
                                message =
                                    "Invalid Email To for supplier " +
                                    supplierInput.supplierName +
                                    ": " +
                                    string.Join(
                                        "; ",
                                        invalidSupplierEmails
                                    )
                            }
                        );
                    }
                }

                List<string> invalidCc =
                    emailCc
                        .Where(email =>
                            !IsValidEmail(email)
                        )
                        .ToList();

                if (invalidCc.Count > 0)
                {
                    return BadRequest(
                        new
                        {
                            success = false,
                            message =
                                "Invalid CC email: " +
                                string.Join(
                                    "; ",
                                    invalidCc
                                )
                        }
                    );
                }

              

                List<IFormFile> attachments =
                    form.Files == null
                        ? new List<IFormFile>()
                        : form.Files.ToList();

                bool result =
                await _supplierPackagesRepository.AssignPackageSuppliers(
        assignTemplate.PackId,
        assignTemplate.SupInputList,
        assignTemplate.ByBoq,
        assignTemplate.UserName,
        attachments,
        assignTemplate.RevisionExpiryDate,
        emailCc,
        CostConn,
        TSConn
    );

                return Ok(
                    new
                    {
                        success = result,

                        message = result
                            ? "Supplier(s) assigned and email sent successfully."
                            : "Supplier assignment or email sending failed."
                    }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(
                    StatusCodes
                        .Status500InternalServerError,
                    new
                    {
                        success = false,
                        message = ex.Message
                    }
                );
            }
        }


        private static List<string> NormalizeEmailList( IEnumerable<string> emails)
        {
            if (emails == null)
            {
                return new List<string>();
            }

            return emails
                .Where(email =>
                    !string.IsNullOrWhiteSpace(email)
                )
                .SelectMany(email =>
                    email.Split(
                        new[]
                        {
                    ',',
                    ';',
                    '\r',
                    '\n'
                        },
                        StringSplitOptions
                            .RemoveEmptyEntries
                    )
                )
                .Select(email =>
                    email.Trim().ToLowerInvariant()
                )
                .Where(email =>
                    !string.IsNullOrWhiteSpace(email)
                )
                .Distinct(
                    StringComparer.OrdinalIgnoreCase
                )
                .ToList();
        }

        private static bool IsValidEmail(
            string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                var address =
                    new System.Net.Mail.MailAddress(
                        email
                    );

                return string.Equals(
                    address.Address,
                    email,
                    StringComparison.OrdinalIgnoreCase
                );
            }
            catch
            {
                return false;
            }
        }

        //[HttpPost("AssignPackageSuppliers")]
        //public async Task<IActionResult> AssignPackageSuppliers(string CostConn, string TSConn)
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        var assignPackageTemplateStr = formCollection["assignPackageTemplate"];
        //        var assignPackageTemplate = JsonConvert.DeserializeObject<AssignPackageTemplateModel>(assignPackageTemplateStr[0]);

        //        List<IFormFile> FileAttachments = formCollection.Files.ToList();
        //        var result = await _supplierPackagesRepository.AssignPackageSuppliers(assignPackageTemplate.packId, assignPackageTemplate.supInputList, assignPackageTemplate.ByBoq, assignPackageTemplate.UserName, FileAttachments, assignPackageTemplate.RevisionExpiryDate, CostConn,  TSConn);

        //        return Ok(new
        //        {
        //            success = true,
        //            data = result
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        string error = ex.ToString();
        //        string path = @"C:\App\error_log.txt";
        //        using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
        //        {
        //            sw.WriteLine(ex.Message+ "  Function:" + ex.TargetSite.Name);
        //        }

        //        return BadRequest(new
        //        {
        //            success = false,
        //            message = ex.Message
        //        });
        //    }
        //}

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
