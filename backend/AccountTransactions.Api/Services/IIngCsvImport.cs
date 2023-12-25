using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services;

public interface IIngCsvImport
{
	Task<List<Transaction>> ReadTransactionsFromCsvFileAsync(Stream stream);
}