using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services;

public interface IBarclaysExcelImport
{
	Task<List<Transaction>> ReadTransactionsFromExcelFileAsync(Stream stream);
}
