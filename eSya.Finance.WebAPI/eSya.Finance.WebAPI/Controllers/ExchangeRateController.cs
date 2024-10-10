using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateRepository _exchangeRateController;
        public ExchangeRateController(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateController = exchangeRateRepository;
        }
        [HttpGet]
        public async Task<IActionResult> FillExchangeRate(string Countrycode)
        {
            var ds = await _exchangeRateController.FillExchangeRate(Countrycode);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoExchangeRate(DO_CurrencyExchangeRate obj)
        {
            var ds = await _exchangeRateController.InsertIntoExchangeRate(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIntoExchangeRate(DO_CurrencyExchangeRate obj)
        {
            var ds = await _exchangeRateController.UpdateIntoExchangeRate(obj);
            return Ok(ds);
        }
    }
}
