using AccountTransactions.Api.Models;
using AccountTransactions.Api.Models.Dtos;
using AccountTransactions.Api.Models.Updater;
using AccountTransactions.Api.Services;
using AccountTransactions.Api.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactions.Api.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
	private readonly ITransactionFileImport fileImport;
	private readonly ITransactionAccess transactionAccess;
	private readonly ITransactionUpdater transactionUpdater;

	public TransactionController(ITransactionAccess transactionAccess, ITransactionFileImport fileImport, ITransactionUpdater transactionUpdater)
	{
		this.fileImport = fileImport;
		this.transactionAccess = transactionAccess;
		this.transactionUpdater = transactionUpdater;
	}

	[HttpGet]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll([FromQuery] Guid? importFileId)
	{
		var transactions = importFileId == null
			? await transactionAccess.GetAllAsync()
			: await transactionAccess.GetAllByImportFileAsync(importFileId.Value);
		if (transactions is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error reading transactions");
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
		Transaction? transaction = await transactionAccess.GetByIdAsync(id);
		if (transaction is null)
		{
			return NotFound();
		}

		TransactionDto dto = transaction.ToDto();

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

		Transaction? transaction = new();
		transactionUpdater.UpdateFromDto(transaction, createDto);

		transaction = await transactionAccess.AddAsync(transaction);

		if (transaction is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error creating transaction");
		}

		TransactionDto dto = transaction.ToDto();

		return CreatedAtAction(nameof(GetById), new {id = dto.Id}, dto);
	}

	[HttpPut("{id:guid}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Update(Guid id, [FromBody] TransactionUpdateDto? updateDto)
	{
		if (updateDto is null)
		{
			return BadRequest("Transaction object not set");
		}

		Transaction? transaction = await transactionAccess.GetByIdAsync(id);
		if (transaction is null)
		{
			return NotFound("Transaction not found");
		}

		transactionUpdater.UpdateFromDto(transaction, updateDto);

		bool success = await transactionAccess.UpdateAsync(transaction);
		if (!success)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error updating transaction");
		}

		return Ok();
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid id)
	{
		Transaction? transaction = await transactionAccess.GetByIdAsync(id);
		if (transaction is null)
		{
			return NotFound("Transaction not found");
		}

		bool success = await transactionAccess.DeleteAsync(transaction);
		if (!success)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting transaction");
		}

		return Ok();
	}

	[HttpPost("import")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<TransactionImportFileDto>> ImportFile([FromForm] TransactionFileUploadDto? importData)
	{
		if (importData is null)
		{
			return BadRequest("Import data not set");
		}

		TransactionImportFile? importedFile = await fileImport.ImportFile(importData);

		if (importedFile is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error importing file");
		}

		TransactionImportFileDto dto = importedFile.ToDto();

		return Ok(dto);
	}
}