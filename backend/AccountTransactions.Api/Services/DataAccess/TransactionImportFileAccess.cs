using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public class TransactionImportFileAccess : ITransactionImportFileAccess
{
    private readonly ITransactionImportFileRepository repository;

    public TransactionImportFileAccess(ITransactionImportFileRepository repository)
    {
        this.repository = repository;

    }

    public async Task<TransactionImportFile?> AddAsync(TransactionImportFile importFile)
    {
        try
        {
            await repository.AddAsync(importFile);
            return importFile;
        }
        catch
        {
            return null;
        }
    }
}
