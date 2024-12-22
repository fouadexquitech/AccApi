using AccApi.Repository.Interfaces;
using AccApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AccApi.Repository.Managers;
using AccApi.Repository.View_Models;
using System.Collections.Generic;
using System.IO;
using System;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckConnectionController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private GlobalLists _globalLists;

        public CheckConnectionController(ILogger<SearchController> logger, GlobalLists globalLists)
        {
            _logger = logger;
            _globalLists = globalLists;
        }

        [HttpPost("CheckConnections")]
        public bool CheckConnections(string CostDbConn)
        {
            try
            {
                if (_globalLists.GetAccDbconnectionString() == null)
                {
                    _globalLists.SetAccDbConnectionString(CostDbConn);

                    string path1 = @"C:\App\conn_log.txt";
                    string dte = DateTime.Now.ToString();
                    using (StreamWriter sw = (System.IO.File.Exists(path1)) ? System.IO.File.AppendText(path1) : System.IO.File.CreateText(path1))
                    {
                        sw.WriteLine("CostDbConn changed to (" + CostDbConn + ") on " + dte);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.ToString();
                string path = @"C:\App\error_log.txt";
                using (StreamWriter sw = (System.IO.File.Exists(path)) ? System.IO.File.AppendText(path) : System.IO.File.CreateText(path))
                {
                    sw.WriteLine(ex.Message);
                }
                return false;
            }
        }

    }
}
