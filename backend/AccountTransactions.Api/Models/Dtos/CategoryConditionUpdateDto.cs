using AccountTransactions.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Models.Dtos;

public class CategoryConditionUpdateDto
{
	[Required]
	[EnumValueDefined(typeof(CategoryConditionType))]
	public CategoryConditionType? Type { get; set; }

	[Required]
	[MinLength(1)]
	public string Text { get; set; } = "";
}