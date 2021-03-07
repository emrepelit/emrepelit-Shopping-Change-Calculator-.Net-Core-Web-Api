using CalculateShoppingChange.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CalculateShoppingChange.Business.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ChangesController : ControllerBase
    {
        private readonly IChangeService _changeService;
        /// <summary>
        /// Dependency (const) injection to reduce dependency for the next future.
        /// </summary>
        /// <param name="changeService"></param>
        public ChangesController(IChangeService changeService)
        {
            _changeService = changeService;
        }
        /// <summary>
        /// Get shopping change.
        /// </summary>
        /// <param name="transaction">asas</param>
        /// <returns></returns>
        [HttpPost("GetChange")]
        public async Task<IActionResult> GetChange([FromBody] Transaction transaction)
        {
            var response = _changeService.GetChange(transaction);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return await Task.FromResult((Ok(response)));
        }

    }
}
