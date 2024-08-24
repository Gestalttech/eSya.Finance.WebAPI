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
        public async Task<IActionResult> FillExchangeRate(int BusinessKey)
        {
            var ds = await _exchangeRateController.FillExchangeRate(BusinessKey);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertUpdateExchangeRate(DO_CurrencyExchangeRate obj)
        {
            var ds = await _exchangeRateController.InsertUpdateExchangeRate(obj);
            return Ok(ds);
        }
    }
}
