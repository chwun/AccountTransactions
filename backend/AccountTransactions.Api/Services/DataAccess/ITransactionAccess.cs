using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public interface ITransactionAccess
{
	Task<IEnumerable<Transaction>?> GetAllAsync();

	Task<Transaction?> GetByIdAsync(Guid id);

	Task<Transaction?> AddAsync(Transaction transaction);
}