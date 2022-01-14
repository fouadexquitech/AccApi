﻿using AccApi.Repository.Interfaces;
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

        [HttpGet("GetLogin")]
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
    }
}
