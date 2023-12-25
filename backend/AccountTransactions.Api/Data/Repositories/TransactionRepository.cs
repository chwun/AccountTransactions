using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Data.Repositories;

public class TransactionRepository : AsyncRepository<Transaction>, ITransactionRepository
{
	public TransactionRepository(AppDbContext context) : base(context)
	{
	}
}