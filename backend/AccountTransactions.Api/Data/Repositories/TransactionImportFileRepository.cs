using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Data.Repositories;

public class TransactionImportFileRepository : AsyncRepository<TransactionImportFile>, ITransactionImportFileRepository
{
	public TransactionImportFileRepository(AppDbContext context) : base(context)
	{
	}
}