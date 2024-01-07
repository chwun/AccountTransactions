using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models.Updater;

public interface ITransactionUpdater
{
	void UpdateFromDto(Transaction transaction, TransactionUpdateDto dto);
}