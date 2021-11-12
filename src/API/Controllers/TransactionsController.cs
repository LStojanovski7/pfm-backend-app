
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Data;
using API.Models;
using System.IO;
using CsvHelper;
using System.Threading.Tasks;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        public TransactionsController()
        {
            
        }

        // [HttpPost("import")]
        // public async Task<ActionResult> Import()
        // {
        //     var file = Request.Form.Files[0];


        //     // using(var reader = new StreamReader(file.OpenReadStream()))
        //     // {
        //     //     string line;

        //     //     while((line = await reader.ReadLineAsync()) != null)
        //     //     {
        //     //         output.Add(line);
        //     //     }
        //     // }


        //     return Ok();
        // }
    }
}