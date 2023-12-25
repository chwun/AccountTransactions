using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Models.Dtos;

public class TransactionUpdateDto
{
	[Required]
	public string SourceOrDestination { get; set; } = "";

	[Required]
	public string Reference { get; set; } = "";

	[Required]
	public TransactionType? Type { get; set; }

	[Required]
	public DateTime? Timestamp { get; set; }

	[Required]
	public decimal? Amount { get; set; }
}