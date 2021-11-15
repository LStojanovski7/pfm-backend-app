
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Data;
using API.Models;
using System.IO;
using CsvHelper;
using System.Threading.Tasks;
using System.Text;
using Services.Transactions;
using Data.Entities.Enums;
using Data.Entities.Contracts;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionServices _transactionService;
        public TransactionsController(ITransactionServices transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder)
        {
            page ??= 1;
            pageSize ??= 10;
            
            return Ok(await _transactionService.GetProducts(page.Value, pageSize.Value, sortBy, sortOrder));
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            var file = Request.Form.Files[0];

            await _transactionService.Import(file.OpenReadStream());

            return Ok("Transactions imported");
        }
    }
}