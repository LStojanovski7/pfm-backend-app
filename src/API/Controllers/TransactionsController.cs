
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

        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            var file = Request.Form.Files[0];

            await _transactionService.Import(file.OpenReadStream());

            return Ok("Transactions imported");
        }
    }
}