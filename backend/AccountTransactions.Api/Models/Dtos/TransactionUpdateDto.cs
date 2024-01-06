using AccountTransactions.Api.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Models.Dtos;

public class TransactionUpdateDto
{
	[Required]
	[MinLength(1)]
	public string SourceOrDestination { get; set; } = "";

	[Required]
	[MinLength(1)]
	public string Reference { get; set; } = "";

	[EnumValueDefined(typeof(TransactionType))]
	public TransactionType Type { get; set; }

	[Required]
	public DateTime? Timestamp { get; set; }

	[Required]
	public decimal? Amount { get; set; }
}