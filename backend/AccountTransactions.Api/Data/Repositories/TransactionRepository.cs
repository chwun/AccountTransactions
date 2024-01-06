using AccountTransactions.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactions.Api.Data.Repositories;

public class TransactionRepository : AsyncRepository<Transaction>, ITransactionRepository
{
	public TransactionRepository(AppDbContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Transaction>> GetAllByImportFileAsNoTrackingAsync(Guid importFileId)
	{
		return await DatabaseContext.Set<Transaction>().Include(t => t.ImportFile).Where(t => t.ImportFileId.Equals(importFileId)).ToListAsync();
	}
}