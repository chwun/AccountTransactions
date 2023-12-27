using AccountTransactions.Api.Data.Repositories;
using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public class CategoryAccess : ICategoryAccess
{
	private readonly ICategoryRepository repository;

	public CategoryAccess(ICategoryRepository repository)
	{
		this.repository = repository;
	}

	public async Task<IEnumerable<Category>?> GetAllAsync()
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
}