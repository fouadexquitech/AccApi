﻿using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
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
    public class ConditionsController : ControllerBase
    {
        private readonly ILogger<ConditionsController> _ilogger;
        private IConditionsRepository _conditionsRepository;
        private ISupplierPackagesRepository _supplierPackagesRepository;

        public ConditionsController(ILogger<ConditionsController> Ilogger, IConditionsRepository conditionsRepository, ISupplierPackagesRepository supplierPackagesRepository)
        {
            _ilogger = Ilogger;
            _conditionsRepository = conditionsRepository;
            _supplierPackagesRepository = supplierPackagesRepository;
        }

        [HttpGet("GetComConditions")]
        public List<ComConditions> GetComConditions(int packSupId)
        {
            try
            {
                return this._conditionsRepository.GetComConditions(packSupId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpPost("AddComConditions")]
        public bool AddComConditions(List<ComConditions> comcond)
        {
            try
            {
                return this._conditionsRepository.AddComConditions(comcond);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateComConditions")]
        public bool UpdateComConditions(ComConditions comcond)
        {
            try
            {
                return this._conditionsRepository.UpdateComConditions(comcond);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("DelComConditions")]
        public bool DelComConditions(int id)
        {
            try
            {
                return this._conditionsRepository.DelComConditions(id);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet("GetTechConditions")]
        public List<TechConditions> GetTechConditions(int packId, string? filter)
        {
            try
            {
                return  this._conditionsRepository.GetTechConditions(packId, filter);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetTechConditionsByPackage")]
        public List<TechConditions> GetTechConditionsByPackage(int packId, int revisionId)
        {
            try
            {
                return this._conditionsRepository.GetTechConditionsByPackage(packId, revisionId);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }


        [HttpPost("AddTechConditions")]
        public bool AddTechConditions(TechConditions techcond)
        {
            try
            {
                return this._conditionsRepository.AddTechConditions(techcond);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateTechConditions")]
        public bool UpdateTechConditions(TechConditions techcond)
        {
            try
            {
                return this._conditionsRepository.UpdateTechConditions(techcond);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("DelTechConditions")]
        public bool DelTechConditions(int id)
        {
            try
            {
                return this._conditionsRepository.DelTechConditions(id);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }


        [HttpGet("GetComCondReplies")]
        public List<DisplayCondition> GetComCondReplies(int packId, string costDB)
        {
            try
            {

                var displayConditions = this._conditionsRepository.GetComConditions(0).Select(x => new DisplayCondition
                {
                    Id = x.cmSeq,
                    Description = x.cmDescription,
                    Replies = new List<DisplayCondReply>()
                }).ToList();

                var listPackageSuppliers = this._supplierPackagesRepository.GetSupplierPackagesList(packId);

                listPackageSuppliers.ForEach(sp =>
                {
                    var replies =  _conditionsRepository.GetComConditionsReply(sp.PsId, costDB);

                    replies.ForEach(x =>
                    {
                        if (x.SupId != null)
                        {
                            DisplayCondReply displayReply = new DisplayCondReply
                            {
                                SupplierId = x.SupId.Value,
                                SupplierName = x.SupName,
                                ConditionId = x.CondId,
                                Reply = x.CondReply
                            };
                            var cond = displayConditions.Where(x => x.Id == displayReply.ConditionId).FirstOrDefault();
                            cond.Replies.Add(displayReply);
                        }
                    });
                });

                return displayConditions;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetTechCondReplies")]
        public List<DisplayCondition> GetTechCondReplies(int packId, string costDB)
        {
            try
            {
                //List<DisplayCondition> displayConditions = this._conditionsRepository.GetTechConditions(packId, null).Select(x => new DisplayCondition
                List<DisplayCondition> displayConditions = this._conditionsRepository.GetTechConditionsByPackage(packId, 0).Select(x => new DisplayCondition
                {
                    Id = x.TcSeq,
                    Description = x.TcDescription,
                    Replies = new List<DisplayCondReply>()
                }).ToList();

                var listPackageSuppliers = this._supplierPackagesRepository.GetSupplierPackagesList(packId);

                listPackageSuppliers.ForEach(sp =>
                {
                    var replies = _conditionsRepository.GetTechConditionsReply(sp.PsId, costDB);

                    replies.ForEach(x =>
                    {
                        if (x.SupId != null)
                        {
                            DisplayCondReply displayReply = new DisplayCondReply
                            {
                                SupplierId = x.SupId.Value,
                                SupplierName = x.SupName,
                                ConditionId = x.CondId,
                                Reply = x.CondReply
                            };
                            var cond = displayConditions.Where(x => x.Id == displayReply.ConditionId).FirstOrDefault();
                            if (cond != null)
                            {
                                cond.Replies.Add(displayReply);
                            }
                        }
                    });
                });

                return displayConditions;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetComConditionsReply")]
        public List<TmpComparisonConditionsReply> GetComConditionsReply(int PackageSupliersID, string costDB)
        {
            try
            {
                return this._conditionsRepository.GetComConditionsReply(PackageSupliersID,costDB);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetTechConditionsReply")]
        public List<TmpComparisonConditionsReply> GetTechConditionsReply(int PackageSupliersID, string costDB)
        {
            try
            {
                return this._conditionsRepository.GetTechConditionsReply(PackageSupliersID,costDB);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }


        [HttpPost("SendTechnicalConditions")]
        public bool SendTechnicalConditions(int packId, TechCondModel techCondModel, string UserName)
        {
            try
            {
                return this._conditionsRepository.SendTechnicalConditions(packId, techCondModel,  UserName);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }


        [HttpPost("UpdateCommercialConditions")]
        public bool UpdateCommercialConditions(int PackageSupliersRevisionID, IFormFile ExcelFile)
        {
            try
            {
                return this._conditionsRepository.UpdateCommercialConditions(PackageSupliersRevisionID, ExcelFile);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpPost("UpdateTechnicalConditions")]
        public bool UpdateTechnicalConditions(int packageId, int PackageSupliersRevisionID, IFormFile ExcelFile)
        {
            try
            {
                return this._conditionsRepository.UpdateTechnicalConditions(packageId, PackageSupliersRevisionID, ExcelFile);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return false;
            }
        }

        [HttpGet("GetComCondReplyByRevision")]
        public List<ConditionsReply> GetComCondReplyByRevision(int revisionid)
        {
            try
            {
                return this._conditionsRepository.GetComCondReplyByRevision(revisionid);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetTechCondReplyByRevision")]
        public List<ConditionsReply> GetTechCondReplyByRevision(int revisionid)
        {
            try
            {
                return this._conditionsRepository.GetTechCondReplyByRevision(revisionid);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

    }
}
