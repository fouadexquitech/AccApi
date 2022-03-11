using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
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
    public class LogonController : ControllerBase
    {
        private readonly ILogger<LogonController> _logger;
        private IlogonRepository _logonRepository;
        
        public LogonController (ILogger<LogonController> logger, IlogonRepository logonRepository)
        {
            _logger = logger;
            _logonRepository = logonRepository;
        }

        [HttpGet("GetProjectCountries")]
        public List<ProjectCountries> GetProjectCountries()
        {
            try
            {
                return this._logonRepository.GetProjectCountries();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetProjects")]
        public List<Project> GetProjects(int dbSeq)
        {
            try
            {
                return this._logonRepository.GetProjects(dbSeq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("GetLogin")]
        public User GetLogin(string user,string pass,int projSeq)
        {
            try
            {
                return this._logonRepository.GetLogin( user,  pass, projSeq);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetProjectCurrency")]
        public ProjectCurrency GetProjectCurrency()
        {
            try
            {
                return this._logonRepository.GetProjectCurrency();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetSuppliersEmailTemplate")]
        public EmailTemplate GetSuppliersEmailTemplate(string Lang)
        {
            try
            {
                return this._logonRepository.GetSuppliersEmailTemplate(Lang);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("SaveEmailTemplate")]
        public bool SaveEmailTemplate(int id, string emailbody)
        {
            try
            {
                return this._logonRepository.SaveEmailTemplate(id,  emailbody);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet("GetManagementEmail")]
        public List<TopManagement> GetManagementEmail()
        {
            try
            {
                return this._logonRepository.GetManagementEmail();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}
