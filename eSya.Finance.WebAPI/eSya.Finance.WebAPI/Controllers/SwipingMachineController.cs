using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SwipingMachineController : ControllerBase
    {
        private readonly ISwipingMachineRepository _swipingMachineRepository;
        public SwipingMachineController(ISwipingMachineRepository swipingMachineRepository)
        {
            _swipingMachineRepository = swipingMachineRepository;
        }
        #region Manage Swip Machine
        [HttpGet]
        public async Task<IActionResult> GetSwipMachinesbyBusinessKey(int businesskey)
        {
            var ds = await _swipingMachineRepository.GetSwipMachinesbyBusinessKey(businesskey);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertAccountGLType(DO_SwipingMachine obj)
        {
            var msg = await _swipingMachineRepository.InsertOrUpdateSwipMachine(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteSwipMachine(DO_SwipingMachine obj)
        {
            var msg = await _swipingMachineRepository.DeleteSwipMachine(obj);
            return Ok(msg);
        }
        #endregion
    }
}
