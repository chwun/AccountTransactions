using AccountTransactions.Api.Models;
using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Services;

public interface ITransactionFileImport
{
	Task<TransactionImportFile?> ImportFile(TransactionFileUploadDto importData);
}