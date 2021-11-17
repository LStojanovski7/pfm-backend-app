
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using API.Models;
using API.Commands;
using System.Threading.Tasks;
using Services.Transactions;
using Data.Entities.Enums;
using Data.Entities.Contracts;
using System.Linq;
using System;

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
        public async Task<IActionResult> GetTransactions([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder,  [FromQuery] string dateFrom = null, [FromQuery] string dateTo = null)
        {
            page ??= 1;
            pageSize ??= 10;

            var result = await _transactionService.GetTransactions(page.Value, pageSize.Value, sortBy, sortOrder);

            var dateF = new DateTime();
            var dateT = new DateTime();

            if(!string.IsNullOrEmpty(dateFrom))
            {
                dateF = DateTime.Parse(dateFrom);
            }

            if(!string.IsNullOrEmpty(dateTo))
            {
                dateT = DateTime.Parse(dateTo);
            }

            var items = result.Items.Where(p => DateTime.Parse(p.Date) > dateF && DateTime.Parse(p.Date) < dateT);

            // result.Items = (List<Data.Entities.Transaction>)items;

            // return Ok(await _transactionService.GetTransactions(page.Value, pageSize.Value, sortBy, sortOrder));
            return Ok(result);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            var file = Request.Form.Files[0];

            await _transactionService.Import(file.OpenReadStream());

            return Ok("Transactions imported");
        }

         [HttpPost("{id}/split")]
        public ActionResult Split(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }

            return Ok("OK");
        }

        // [HttpPost("{id}/categorize")]
        [HttpPost]
        [Route("{id}/categorize")]
        public async Task<ActionResult> Categorize([FromRoute] string id, [FromBody] Categorize command)
        {
            var result = await _transactionService.Categorize(id, command.catcode); 

            if(result == null)
            {
                return NotFound();
            }

            return Ok("OK - Transaction categorized");
        }

        [HttpPost("/auto-categorize")]
        public ActionResult AutoCategorize()
        {
            return Ok();
        }
    }
}