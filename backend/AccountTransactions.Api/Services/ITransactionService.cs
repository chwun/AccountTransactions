using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services;

public interface ITransactionService
{
    Task<IEnumerable<Transaction>?> GetAllAsync();

    Task<Transaction?> GetByIdAsync(Guid id);

    Task<Transaction?> AddAsync(Transaction transaction);
}
