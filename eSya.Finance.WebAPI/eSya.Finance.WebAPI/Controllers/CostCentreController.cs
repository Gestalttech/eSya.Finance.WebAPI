using eSya.Finance.DL.Repository;
using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CostCentreController : ControllerBase
    {
        private readonly ICostCentreRepository _costCentreController;
        public CostCentreController(ICostCentreRepository costCentreRepository)
        {
            _costCentreController = costCentreRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCostCenterList()
        {
            var ds = await _costCentreController.GetCostCenterList();
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCostCenter(DO_CostCenter obj)
        {
            var ds = await _costCentreController.CreateCostCenter(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCostCenter(DO_CostCenter obj)
        {
            var ds = await _costCentreController.UpdateCostCenter(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCostCenter(int CostCenterCode)
        {
            var ds = await _costCentreController.DeleteCostCenter(CostCenterCode);
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetCostCenterClassForComobo()
        {
            var ds = await _costCentreController.GetCostCenterClassForComobo();
            return Ok(ds);
        }

        [HttpGet]
        public async Task<IActionResult> GetCostCenterClass()
        {
            var ds = await _costCentreController.GetCostCenterClass();
            return Ok(ds);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCostCenterClass(DO_CostCenterClass obj)
        {
            var ds = await _costCentreController.CreateCostCenterClass(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCostCenterClass(DO_CostCenterClass obj)
        {
            var ds = await _costCentreController.UpdateCostCenterClass(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteCostCenterClass(int CostCenterClass)
        {
            var ds = await _costCentreController.DeleteCostCenterClass(CostCenterClass);
            return Ok(ds);
        }
    }
}
