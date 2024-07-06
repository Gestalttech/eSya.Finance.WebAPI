using eSya.Finance.DL.Repository;
using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountGroupController : ControllerBase
    {
        private readonly IAccountGroupRepository _accountGroupRepository;
        public AccountGroupController(IAccountGroupRepository accountGroupRepository)
        {
            _accountGroupRepository = accountGroupRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetActiveBookTypes()
        {
            var ds = await _accountGroupRepository.GetActiveBookTypes();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountGroupsforTreeview()
        {
            var ds = await _accountGroupRepository.GetAccountGroupsforTreeview();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAccountGroup(string groupcode)
        {
            var ds = await _accountGroupRepository.DeleteAccountGroup(groupcode);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoAccountGroup(DO_AccountGroup obj)
        {
            var ds = await _accountGroupRepository.InsertIntoAccountGroup(obj);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAccountGroup(DO_AccountGroup obj)
        {
            var ds = await _accountGroupRepository.UpdateAccountGroup(obj);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> AccountGroupMoveUpDown(string GroupCode, string ParentID, short GroupIndex, bool moveUp)
        {
            var ds = await _accountGroupRepository.AccountGroupMoveUpDown(GroupCode, ParentID, GroupIndex, moveUp);
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetAccountGroupsbyGroupCode(string groupcode)
        {
            var ds = await _accountGroupRepository.GetAccountGroupsbyGroupCode(groupcode);
            return Ok(ds);
        }
    }
}
