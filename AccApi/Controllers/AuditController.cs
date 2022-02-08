using AccApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly ILogger<LogonController> _logger;
        private IAuditRepository _auditRepository;

        public AuditController(ILogger<LogonController> logger, IAuditRepository auditRepository)
        {
            _logger = logger;
            _auditRepository = auditRepository;
        }

        [HttpPost("PostAuditLog")]
        public bool SetAuditLog(string tablename, string userid,string datetime, string action, string primarykeyvalue)
        {
            try
            {
                return this._auditRepository.SetAuditLog(tablename,  userid, Convert.ToDateTime(datetime),  action,  primarykeyvalue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }


        [HttpPost("PostLoginLog")]
        public bool SetLoginLog( string userid, string datetime, string ip, string pcName)
        {
            try
            {
                return this._auditRepository.SetLoginLog( userid, Convert.ToDateTime(datetime), ip, pcName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
