﻿using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
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
    public class ConditionsController : ControllerBase
    {
        private readonly ILogger<ConditionsController> _ilogger;
        private IConditionsRepository _conditionsRepository;

        public ConditionsController(ILogger<ConditionsController> Ilogger, IConditionsRepository conditionsRepository)
        {
            _ilogger = Ilogger;
            _conditionsRepository = conditionsRepository;
        }

        [HttpGet("GetComConditions")]
        public List<TblComCond> GetComConditions()
        {
            try
            {
                return this._conditionsRepository.GetComConditions();
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetTechConditions")]
        public List<TblTechCond> GetTechConditions(int packId)
        {
            try
            {
                return this._conditionsRepository.GetTechConditions(packId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }


        [HttpGet("SendTechnicalConditions")]
        public bool SendTechnicalConditions(int packId)
        {
            try
            {
                return this._conditionsRepository.SendTechnicalConditions(packId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }


        [HttpPost("UpdateCommercialConditions")]
        public bool UpdateCommercialConditions(int PackageSupliersID, IFormFile ExcelFile)
        {
            try
            {
                return this._conditionsRepository.UpdateCommercialConditions( PackageSupliersID,ExcelFile);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateTechnicalConditions")]
        public bool UpdateTechnicalConditions(int PackageSupliersID, IFormFile ExcelFile)
        {
            try
            {
                return this._conditionsRepository.UpdateTechnicalConditions(PackageSupliersID, ExcelFile);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }


    }
}