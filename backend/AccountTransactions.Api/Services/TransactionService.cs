
using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository repository;

    public TransactionService(ITransactionRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Transaction>?> GetAllAsync()
    {
        try
        {
            return await repository.GetAllAsNoTrackingAsync();
        }
        catch
        {
            return null;
        }
    }
}
