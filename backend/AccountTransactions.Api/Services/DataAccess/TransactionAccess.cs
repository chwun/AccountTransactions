using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public class TransactionAccess : ITransactionAccess
{
	private readonly ITransactionRepository repository;

	public TransactionAccess(ITransactionRepository repository)
	{
		this.repository = repository;
	}

	public async Task<IEnumerable<Transaction>?> GetAllAsync()
	{
		try
		{
			return await repository.GetAllAsNoTrackingAsync();
		}
		catch
		{
			return null;
		}
	}

	public async Task<IEnumerable<Transaction>?> GetAllByImportFileAsync(Guid importFileId)
	{
		try
		{
			return await repository.GetAllByImportFileAsNoTrackingAsync(importFileId);
		}
		catch
		{
			return null;
		}
	}

	public async Task<Transaction?> GetByIdAsync(Guid id)
	{
		try
		{
			return await repository.GetAsync(id);
		}
		catch
		{
			return null;
		}
	}

	public async Task<Transaction?> AddAsync(Transaction transaction)
	{
		try
		{
			await repository.AddAsync(transaction);
			return transaction;
		}
		catch
		{
			return null;
		}
	}

	public async Task<bool> UpdateAsync(Transaction transaction)
	{
		try
		{
			await repository.UpdateAsync(transaction);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> DeleteAsync(Transaction transaction)
	{
		try
		{
			await repository.RemoveAsync(transaction);
			return true;
		}
		catch
		{
			return false;
		}
	}
}