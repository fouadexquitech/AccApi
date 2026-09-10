using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AccApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IDashboardRepository dashboardRepository, ILogger<DashboardController> logger)
        {
            _dashboardRepository = dashboardRepository;
            _logger = logger;
        }

        [HttpGet("GetProjectTotalBudget")]
        public ActionResult<double> GetProjectTotalBudget(string costConn)
        {
            try { return Ok(_dashboardRepository.GetProjectTotalBudget(costConn)); }
            catch (System.Exception ex) { _logger.LogError(ex.Message); return StatusCode(500, ex.Message); }
        }

        [HttpGet("GetPackagesBudget")]
        public ActionResult<List<PackageBudgetItem>> GetPackagesBudget(string costConn)
        {
            try { return Ok(_dashboardRepository.GetPackagesBudget(costConn)); }
            catch (System.Exception ex) { _logger.LogError(ex.Message); return StatusCode(500, ex.Message); }
        }

        [HttpGet("GetMissingByDivision")]
        public ActionResult<List<DivisionMissingItem>> GetMissingByDivision(string costConn)
        {
            try { return Ok(_dashboardRepository.GetMissingByDivision(costConn)); }
            catch (System.Exception ex) { _logger.LogError(ex.Message); return StatusCode(500, ex.Message); }
        }

        [HttpGet("GetMissingResourcesForDivision")]
        public ActionResult<List<DivisionResourceItem>> GetMissingResourcesForDivision(string costConn, string division)
        {
            try { return Ok(_dashboardRepository.GetMissingResourcesForDivision(costConn, division)); }
            catch (System.Exception ex) { _logger.LogError(ex.Message); return StatusCode(500, ex.Message); }
        }

        [HttpGet("GetQuotationBudget")]
        public ActionResult<List<QuotationSupplierItem>> GetQuotationBudget(string costConn)
        {
            try { return Ok(_dashboardRepository.GetQuotationBudget(costConn)); }
            catch (System.Exception ex) { _logger.LogError(ex.Message); return StatusCode(500, ex.Message); }
        }

        [HttpGet("GetBudgetByDivision")]
        public ActionResult<List<DivisionBudgetItem>> GetBudgetByDivision(string costConn)
        {
            try { return Ok(_dashboardRepository.GetBudgetByDivision(costConn)); }
            catch (System.Exception ex) { _logger.LogError(ex.Message); return StatusCode(500, ex.Message); }
        }
    }
}
