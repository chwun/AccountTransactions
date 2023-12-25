using AccountTransactions.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactions.Api.Models;

public class Transaction
{
	public Guid Id { get; set; }

	public string SourceOrDestination { get; set; } = "";

	public string Reference { get; set; } = "";

	public TransactionType Type { get; set; }

	public DateTime Timestamp { get; set; }

	[Precision(9, 2)]
	public decimal Amount { get; set; }

	public TransactionImportFile? ImportFile { get; set; }

	public Guid ImportFileId { get; set; }

	public TransactionDto ToDto() => new()
	{
		Id = Id,
		SourceOrDestination = SourceOrDestination,
		Reference = Reference,
		Type = Type,
		Timestamp = Timestamp,
		Amount = Amount
	};

	public static Transaction FromUpdateDto(TransactionUpdateDto dto) => new()
	{
		SourceOrDestination = dto.SourceOrDestination,
		Reference = dto.Reference,
		Type = dto.Type ?? throw new InvalidDataException(),
		Timestamp = dto.Timestamp ?? throw new InvalidDataException(),
		Amount = dto.Amount ?? throw new InvalidDataException()
	};
}