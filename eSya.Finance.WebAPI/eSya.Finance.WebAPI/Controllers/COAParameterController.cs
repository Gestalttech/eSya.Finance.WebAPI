using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class COAParameterController : ControllerBase
    {
        private readonly ICOAParameterRepository _coaParameterController;
        public COAParameterController(ICOAParameterRepository coaParameterRepository)
        {
            _coaParameterController = coaParameterRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountGLType()
        {
            var ds = await _coaParameterController.GetAccountGLType();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetGLTypeDescription()
        {
            var ds = await _coaParameterController.GetGLTypeDescription();
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAccountGLType(DO_COAParameter obj)
        {
            var ds = await _coaParameterController.InsertAccountGLType(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAccountGLType(DO_COAParameter obj)
        {
            var ds = await _coaParameterController.UpdateAccountGLType(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccountGLType(int AccountSgltype)
        {
            var ds = await _coaParameterController.DeleteAccountGLType(AccountSgltype);
            return Ok(ds);
        }
    }
}
