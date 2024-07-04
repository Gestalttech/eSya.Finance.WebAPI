using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherTypeController : ControllerBase
    {
        private readonly IVoucherTypeRepository _booktypeRepository;
        public VoucherTypeController(IVoucherTypeRepository booktypeRepository)
        {
            _booktypeRepository = booktypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetActiveBookTypes()
        {
            var ds = await _booktypeRepository.GetActiveBookTypes();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetBookTypePaymentMethods(string booktype, string? vouchertype)
        {
            var ds = await _booktypeRepository.GetBookTypePaymentMethods(booktype, vouchertype);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetVoucherTypesbyBookType(string booktype)
        {
            var ds = await _booktypeRepository.GetVoucherTypesbyBookType(booktype);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> ChkBookTypePaymentMethodLinkRequried(string booktype)
        {
            var ds = await _booktypeRepository.ChkBookTypePaymentMethodLinkRequried(booktype);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateVoucherType(DO_VoucherType obj)
        {
            var ds = await _booktypeRepository.InsertOrUpdateVoucherType(obj);
            return Ok(ds);
        }
    }
}
