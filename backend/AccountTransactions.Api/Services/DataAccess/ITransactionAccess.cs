using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public interface ITransactionAccess
{
	Task<IEnumerable<Transaction>?> GetAllAsync();

	Task<IEnumerable<Transaction>?> GetAllByImportFileAsync(Guid importFileId);

	Task<Transaction?> GetByIdAsync(Guid id);

	Task<Transaction?> AddAsync(Transaction transaction);

	Task<bool> UpdateAsync(Transaction transaction);

	Task<bool> DeleteAsync(Transaction transaction);
}