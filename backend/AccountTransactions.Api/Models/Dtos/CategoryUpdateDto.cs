using AccountTransactions.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Models.Dtos;

public class CategoryUpdateDto
{
	[Required]
	[MinLength(1)]
	public string Name { get; set; } = "";

	[EnumValueDefined(typeof(TransactionType))]
	public TransactionType TransactionType { get; set; }

	[Required]
	public List<CategoryConditionUpdateDto> Conditions { get; init; } = [];
}