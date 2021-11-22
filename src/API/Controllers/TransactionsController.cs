
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
using Data.Entities;
using System.Globalization;

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
        public async Task<IActionResult> GetTransactions([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder, [FromQuery] string dateFrom = null, [FromQuery] string dateTo = null)
        {
            page ??= 1;
            pageSize ??= 10;

            var result = await _transactionService.GetTransactions(page.Value, pageSize.Value, sortBy, sortOrder);

            List<TransactionWithSplits> items = new List<TransactionWithSplits>();

            DateTime? dateF = null;
            DateTime? dateT = null;

            if(!string.IsNullOrEmpty(dateFrom))
            {
                dateF = DateTime.ParseExact(dateFrom, "M/d/yyyy", CultureInfo.InvariantCulture);
                // dateF = DateTime.Parse(dateFrom);
            }
            else
            {
                dateF = DateTime.MinValue;
            }

            if(!string.IsNullOrEmpty(dateTo))
            {
                dateT = DateTime.ParseExact(dateTo, "M/d/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                dateT = DateTime.MaxValue;
            }

            items = result.Items.Where(t => DateTime.ParseExact(t.Date, "M/d/yyyy", CultureInfo.InvariantCulture) >= dateF && DateTime.ParseExact(t.Date, "M/d/yyyy", CultureInfo.InvariantCulture) <= dateT).ToList();

            result.Items = items;

            return Ok(result);
        }

        [HttpPost("import")]
        public async Task<ActionResult> Import()
        {
            var file = Request.Form.Files[0];

            await _transactionService.Import(file.OpenReadStream());

            return Ok("Transactions imported");
            // return Ok("{}");
        }

        [HttpPost("{id}/split")]
        public async Task<ActionResult> Split([FromRoute] string id, [FromBody] SplitTransactionCommand command)
        {
            if (string.IsNullOrEmpty(id) || command == null)
            {
                return BadRequest();
            }

            await _transactionService.Split(id, command.Splits);

            return Ok("{}");
        }

        [HttpPost]
        [Route("{id}/categorize")]
        public async Task<ActionResult> Categorize([FromRoute] string id, [FromBody] CategorizeCommand command)
        {
            var result = await _transactionService.Categorize(id, command.catcode);

            if (result == null)
            {
                return NotFound();
            }

            // return Ok("OK - Transaction categorized");
            return Ok(new {value = "OK"});
        }

        [HttpPost("/auto-categorize")]
        public ActionResult AutoCategorize()
        {
            return Ok();
        }
    }
}