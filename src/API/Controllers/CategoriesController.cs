using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using CsvHelper.Configuration;
using Data;
using Data.Entities;
using Services.Categories;
using System.Globalization;
using System.Linq;
// using API.CsvMaps;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        public CategoriesController(ICategoryServices categoryService)
        {
            _categoryService = categoryService;   
        }

        [HttpGet]
        public async Task<ActionResult> GetTransactions([FromQuery] string parrentId = null)
        {
            return Ok(await _categoryService.GetCategories(parrentId));
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            var file = Request.Form.Files[0];

            if(file.ContentType != "text/csv")
            {
                // Return response error
                return BadRequest("The file must be of type: (csv)");
            }

            await _categoryService.ImportCategories(file.OpenReadStream());

            return Ok("OK Transactions imported");
        }
    }
}