using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using NBP.Data;

namespace NBP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NBPController : ControllerBase
    {
        private readonly INBPRepository _repository;
        public NBPController(INBPRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<NBPTable> GetNBPTable(string type)
        {
            try
            {
                var result = await _repository.GetNBPTable(type);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
