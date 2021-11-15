using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using CsvHelper;
using CsvHelper.Configuration;
using Services.MerchantTypes;
using System.Globalization;
using System.Linq;
// using API.CsvMaps;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MccController : ControllerBase
    {
        private readonly IMerchantTypeService _merchantTypeService;
        public MccController(IMerchantTypeService merchantTypeService)
        {
            _merchantTypeService = merchantTypeService;   
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

            await _merchantTypeService.Import(file.OpenReadStream());

            return Ok("OK MCC codes are imported");
        }
    }
}