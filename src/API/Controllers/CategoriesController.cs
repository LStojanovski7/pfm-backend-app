using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Data;
using Data.Entities;
using API.Models;
using Services.Categories;
using System.Globalization;
using System.Linq;
using AutoMapper;
using System.Net.Http;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryServices categoryService, IMapper mapper)
        {
            _categoryService = categoryService;   
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string parrentId = null)
        {
            CategoriesList categories = new CategoriesList();
            List<CategoryModel> items = new List<CategoryModel>();

            var result = await _categoryService.GetCategories(parrentId);

            foreach(var item in result)
            {
                CategoryModel category = new CategoryModel();

                items.Add(_mapper.Map(item, category));
            }

            categories.Items = items;

            return Ok(categories);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            var file = Request.Form.Files[0];

            if(file.ContentType != "text/csv")
            {
               BadRequest("Your request was not constructed properly. Response contains the list of validation errors for each invalid element of your request. To learn more see general guidance on validation");
            }

            await _categoryService.Import(file.OpenReadStream());

            return Ok("Categories imported");
        }
    }
}