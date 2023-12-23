using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Models.Dtos;
using AccountTransactions.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactions.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        this.transactionService = transactionService;

    }

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
    {
        var transactions = await transactionService.GetAllAsync();
        if (transactions is null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error reading all transactions");
        }

        var dtos = transactions.Select(x => x.ToDto());

        return Ok(dtos);
    }
}
