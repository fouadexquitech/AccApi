using AccApi.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ILogger<CurrencyConverterController> _logger;
        private readonly ICurrencyConverterRepository _currencyConverterRepository;

        public CurrencyConverterController(ILogger<CurrencyConverterController> logger, ICurrencyConverterRepository currencyConverterRepository)
        {
            _logger = logger;
            _currencyConverterRepository = currencyConverterRepository;
        }

        [HttpGet("GetCurrencyExchange")]
        public decimal GetCurrencyExchange(string localCurrency, string foreignCurrency)
        {
            try
            {   
                return this._currencyConverterRepository.GetCurrencyExchange(localCurrency,foreignCurrency);               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return 0;
            }
        }

    }
}
