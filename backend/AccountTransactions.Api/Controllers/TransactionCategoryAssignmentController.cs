using AccountTransactions.Api.Helpers;
using AccountTransactions.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Controllers;

[ApiController]
[Route("api/transactions/assignment")]
public class TransactionCategoryAssignmentController : ControllerBase
{
	private readonly ICategoryAssignment categoryAssignment;

	public TransactionCategoryAssignmentController(ICategoryAssignment categoryAssignment)
	{
		this.categoryAssignment = categoryAssignment;
	}

	[HttpPost]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> AutoAssignByImportFile([FromQuery] [Required] [GuidNotEmpty] Guid? importFileId)
	{
		await categoryAssignment.AssignTransactionsOfImportFile(importFileId!.Value);

		return Ok();
	}
}