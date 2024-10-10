using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommonDataController : ControllerBase
    {
        private readonly ICommonDataRepository _commonDataRepository;
        public CommonDataController(ICommonDataRepository commonDataRepository)
        {
            _commonDataRepository = commonDataRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetApplicationCodesByCodeType(int codeType)
        {
            var ds = await _commonDataRepository.GetApplicationCodesByCodeType(codeType);
            return Ok(ds);
        }

        [HttpPost]
        public async Task<IActionResult> GetApplicationCodesByCodeTypeList(List<int> l_codeType)
        {
            var ds = await _commonDataRepository.GetApplicationCodesByCodeTypeList(l_codeType);
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetBusinessKey()
        {
            var ds = await _commonDataRepository.GetBusinessKey();
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveCurrencyCodes(int BusinessKey)
        {
            var ds = await _commonDataRepository.GetActiveCurrencyCodes(BusinessKey);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveCountryCodes()
        {
            var ds = await _commonDataRepository.GetActiveCountryCodes();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveExchangeCurrencyCodes(string Countrycode)
        {
            var ds = await _commonDataRepository.GetActiveExchangeCurrencyCodes(Countrycode);
            return Ok(ds);
        }

    }
}
