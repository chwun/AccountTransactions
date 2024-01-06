using AccountTransactions.Api.Models;
using AccountTransactions.Api.Models.Dtos;
using AccountTransactions.Api.Models.Updater;
using AccountTransactions.Api.Services.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace AccountTransactions.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
	private readonly ICategoryAccess categoryAccess;
	private readonly ICategoryUpdater categoryUpdater;

	public CategoryController(ICategoryAccess categoryAccess, ICategoryUpdater categoryUpdater)
	{
		this.categoryAccess = categoryAccess;
		this.categoryUpdater = categoryUpdater;
	}

	[HttpGet]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
	{
		var categories = await categoryAccess.GetAllAsync();
		if (categories is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error reading categories");
		}

		var dtos = categories.Select(x => x.ToDto());

		return Ok(dtos);
	}

	[HttpGet("{id:guid}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<ActionResult<TransactionDto>> GetById(Guid id)
	{
		Category? category = await categoryAccess.GetByIdAsync(id);
		if (category is null)
		{
			return NotFound();
		}

		CategoryDto dto = category.ToDto();

		return Ok(dto);
	}

	[HttpPost]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<ActionResult<CategoryDto>> Add([FromBody] CategoryUpdateDto? createDto)
	{
		if (createDto is null)
		{
			return BadRequest("Category object not set");
		}

		Category? category = new();
		categoryUpdater.UpdateFromDto(category, createDto);

		category = await categoryAccess.AddAsync(category);

		if (category is null)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error creating category");
		}

		CategoryDto dto = category.ToDto();

		return CreatedAtAction(nameof(GetById), new {id = dto.Id}, dto);
	}

	[HttpPut("{id:guid}")]
	[Produces("application/json")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateDto? updateDto)
	{
		if (updateDto is null)
		{
			return BadRequest("Category object not set");
		}

		Category? category = await categoryAccess.GetByIdAsync(id);
		if (category is null)
		{
			return NotFound("Category not found");
		}

		categoryUpdater.UpdateFromDto(category, updateDto);

		bool success = await categoryAccess.UpdateAsync(category);
		if (!success)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error updating category");
		}

		return Ok();
	}

	[HttpDelete("{id:guid}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[ProducesResponseType(StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid id)
	{
		Category? category = await categoryAccess.GetByIdAsync(id);
		if (category is null)
		{
			return NotFound("Category not found");
		}

		bool success = await categoryAccess.DeleteAsync(category);
		if (!success)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting category");
		}

		return Ok();
	}
}