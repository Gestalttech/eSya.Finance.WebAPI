using eSya.Finance.DL.Repository;
using eSya.Finance.DO;
using eSya.Finance.IF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eSya.Finance.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookTypeController : ControllerBase
    {
        private readonly IBookTypeRepository _booktypeRepository;
        public BookTypeController(IBookTypeRepository booktypeRepository)
        {
            _booktypeRepository = booktypeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetBookTypes()
        {
            var ds = await _booktypeRepository.GetBookTypes();
            return Ok(ds);
        }
        [HttpGet]
        public async Task<IActionResult> GetBooksbyType(string booktype)
        {
            var ds = await _booktypeRepository.GetBooksbyType(booktype);
            return Ok(ds);
        }
        [HttpPost]
        public async Task<IActionResult> InsertIntoBookType(DO_BookType obj)
        {
            var msg = await _booktypeRepository.InsertIntoBookType(obj);
            return Ok(msg);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBookType(DO_BookType obj)
        {
            var msg = await _booktypeRepository.UpdateBookType(obj);
            return Ok(msg);
        }
    }
}
