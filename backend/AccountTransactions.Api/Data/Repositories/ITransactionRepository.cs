using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Data.Repositories;

public interface ITransactionRepository : IAsyncRepository<Transaction>
{
}