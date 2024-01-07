namespace AccountTransactions.Api.Services;

public interface ICategoryAssignment
{
	Task<bool> AssignTransactionsOfImportFile(Guid importFileId);
}