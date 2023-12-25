namespace AccountTransactions.Api.Models.Dtos;

public class TransactionDto
{
	public Guid Id { get; set; }

	public string SourceOrDestination { get; set; } = "";

	public string Reference { get; set; } = "";

	public TransactionType Type { get; set; }

	public DateTime Timestamp { get; set; }

	public decimal Amount { get; set; }
}