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
        [HttpGet]
        public async Task<IActionResult> GetCostCenterCodes(int CostCentreCode)
        {
            var ds = await _costCentreController.GetCostCenterCodes(CostCentreCode);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCostCenterCodes(DO_CostCenter obj)
        {
            if (obj.IsInsert == 1)
            {
                var ds = await _costCentreController.CreateCostCenter(obj);
                return Ok(ds);
            }
            else
            {
                var ds = await _costCentreController.UpdateCostCenter(obj);
                return Ok(ds);
            }
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> GetCostCenterClassByCode(int CostCenterClass)
        {
            var ds = await _costCentreController.GetCostCenterClassByCode(CostCenterClass);
            return Ok(ds);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCostCenterClass(DO_CostCenterClass obj)
        {
            if (obj.IsInsert == 1)
            {
                var ds = await _costCentreController.CreateCostCenterClass(obj);
                return Ok(ds);
            }
            else
            {
                var ds = await _costCentreController.UpdateCostCenterClass(obj);
                return Ok(ds);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCostCenterClass(int CostCenterClass)
        {
            var ds = await _costCentreController.DeleteCostCenterClass(CostCenterClass);
            return Ok(ds);
        }
    }
}
