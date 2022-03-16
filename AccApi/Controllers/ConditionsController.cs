using AccApi.Repository.Interfaces;
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
        public List<ComConditions> GetComConditions()
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
        public List<TechConditions> GetTechConditions(int packId)
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

        [HttpPost("AddTechConditions")]
        public bool AddTechConditions(List<TechConditions> techcond)
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


        [HttpGet("GetTechCondReplies")]
        public List<DisplayCondition> GetTechCondReplies(int packId)
        {
            try
            {

                List<DisplayCondition> displayConditions = this._conditionsRepository.GetTechConditions(packId).Select(x => new DisplayCondition
                {
                    Id = x.TcSeq,
                    Description = x.TcDescription,
                    Replies = new List<DisplayCondReply>()
                }).ToList();

                var listPackageSuppliers = this._supplierPackagesRepository.SupplierPackagesList(packId);
                listPackageSuppliers.ForEach(sp =>
                {
                    var replies = _conditionsRepository.GetTechConditionsReply(sp.PsId);

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


        [HttpGet("GetComCondReplies")]
        public List<DisplayCondition> GetComCondReplies(int packId)
        {
            try
            {

                List<DisplayCondition> displayConditions = this._conditionsRepository.GetComConditions().Select(x => new DisplayCondition
                {
                    Id = x.CmSeq,
                    Description = x.CmDescription,
                    Replies = new List<DisplayCondReply>()
                }).ToList();

                var listPackageSuppliers = this._supplierPackagesRepository.SupplierPackagesList(packId);
                listPackageSuppliers.ForEach(sp =>
                {
                    var replies = _conditionsRepository.GetComConditionsReply(sp.PsId);

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

        [HttpGet("GetComConditionsReply")]
        public List<TmpConditionsReply> GetComConditionsReply(int PackageSupliersID)
        {
            try
            {
                return this._conditionsRepository.GetComConditionsReply(PackageSupliersID);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }

        [HttpGet("GetTechConditionsReply")]
        public List<TmpConditionsReply> GetTechConditionsReply(int PackageSupliersID)
        {
            try
            {
                return this._conditionsRepository.GetTechConditionsReply(PackageSupliersID);
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex.Message);
                return null;
            }
        }


        [HttpPost("SendTechnicalConditions")]
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
                return this._conditionsRepository.UpdateCommercialConditions(PackageSupliersID, ExcelFile);
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
