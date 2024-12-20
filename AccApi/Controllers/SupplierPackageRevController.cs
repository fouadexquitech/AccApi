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
    public class SupplierPackagesRevController : ControllerBase
    {
        private readonly ILogger<SupplierPackagesRevController> _ilogger;
        private ISupplierPackagesRevRepository _supplierPackagesRevRepository;

        public SupplierPackagesRevController(ILogger<SupplierPackagesRevController> ilogger, ISupplierPackagesRevRepository supplierPackagesRevRepository)
        {
            _ilogger = ilogger;
            _supplierPackagesRevRepository = supplierPackagesRevRepository;
        }

        [HttpGet("GetSupplierPackagesRevision")]
        public List<SupplierPackagesRevList> GetSupplierPackagesRevision(int packageSupplierId)
        {
            try
            {
                return this._supplierPackagesRevRepository.GetSupplierPackagesRevList(packageSupplierId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpGet("GetSupplierPackagesSingleRevision")]
        public SupplierPackagesRevList GetSupplierPackagesSingleRevision(int revisionId)
        {
            try
            {
                return this._supplierPackagesRevRepository.GetSupplierPackagesRevision(revisionId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpPost("AddField")]
        public decimal? AddField(int revId, string lbl, double val, int type)
        {
            try
            {
                return this._supplierPackagesRevRepository.AddField(revId, lbl, val,type);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpGet("GetCurrencies")]
        public List<CurrencyList> GetCurrencies()
        {
            try
            {
                return this._supplierPackagesRevRepository.GetCurrencies( );
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }

        [HttpPost("DeleteField")]
        public bool DeleteField(int fieldId)
        {
            try
            {
                return this._supplierPackagesRevRepository.DeleteField(fieldId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

       [HttpGet("GetFields")]
       public List<RevisionFieldsList> GetFields(int revisionid)
        {
            try
            {
                return this._supplierPackagesRevRepository.GetFields(revisionid);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return null;
            }
        }
    }
}
