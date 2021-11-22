using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Categories;

namespace API.Controllers
{
    [ApiController]
    [Route("spending-analytics")]
    public class SpendingAnalytics : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        public SpendingAnalytics(ICategoryServices categoryServices)
        {
            _categoryService = categoryServices;
        }

        [HttpGet]
        public async Task<ActionResult> SpendingByCategory([FromQuery] string catcode, [FromQuery] string startDate, 
                                                           [FromQuery]string endDate, [FromQuery] string direction)
        {
            var response = await _categoryService.SpendingByCategory(catcode, startDate, endDate, direction);

            return Ok(response);
        }
    }
}