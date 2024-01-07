using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models.Updater;

public class TransactionUpdater : ITransactionUpdater
{
	public void UpdateFromDto(Transaction transaction, TransactionUpdateDto dto)
	{
		transaction.SourceOrDestination = dto.SourceOrDestination;
		transaction.Reference = dto.Reference;
		transaction.Type = dto.Type ?? throw new InvalidDataException();
		transaction.Timestamp = dto.Timestamp ?? throw new InvalidDataException();
		transaction.Amount = dto.Amount ?? throw new InvalidDataException();
	}
}