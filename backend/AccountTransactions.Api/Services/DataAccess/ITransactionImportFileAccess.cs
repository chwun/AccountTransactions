using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public interface ITransactionImportFileAccess
{
    Task<TransactionImportFile?> AddAsync(TransactionImportFile importFile);
}
