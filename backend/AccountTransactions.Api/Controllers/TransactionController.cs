using AccountTransactions.Api.Models;
using AccountTransactions.Api.Models.Dtos;
using AccountTransactions.Api.Services;
using AccountTransactions.Api.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactions.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
	private readonly ITransactionAccess transactionAccess;
	private readonly ITransactionFileImport fileImport;

	public TransactionController(ITransactionAccess transactionAccess, ITransactionFileImport fileImport)
	{
		this.fileImport = fileImport;
		this.transactionAccess = transactionAccess;
	}

	[HttpGet]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
	{
		var transactions = await transactionAccess.GetAllAsync();
		if (transactions is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error reading all transactions");
		}

		var dtos = transactions.Select(x => x.ToDto());

		return Ok(dtos);
	}

	[HttpGet("{id:guid}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<TransactionDto>> GetById(Guid id)
	{
		var transaction = await transactionAccess.GetByIdAsync(id);
		if (transaction is null)
		{
			return NotFound();
		}

		var dto = transaction.ToDto();

		return Ok(dto);
	}

	[HttpPost]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<TransactionDto>> Add([FromBody] TransactionUpdateDto? createDto)
	{
		if (createDto is null)
		{
			return BadRequest("Transaction object not set");
		}

		var transaction = Transaction.FromUpdateDto(createDto);
		transaction = await transactionAccess.AddAsync(transaction);

		if (transaction is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error creating transaction");
		}

		var dto = transaction.ToDto();

		return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
	}

	[HttpPost("import")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<TransactionImportFileDto>> Add([FromForm] TransactionFileUploadDto? importData)
	{
		if (importData is null)
		{
			return BadRequest("Import data not set");
		}

		var importedFile = await fileImport.ImportFile(importData);

		if (importedFile is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error importing file");
		}

		var dto = importedFile.ToDto();

		return Ok(dto);
	}
}
