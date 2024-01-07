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

	public async Task<IEnumerable<Category>?> GetAllWithConditionsAsync()
	{
		try
		{
			return await repository.GetAllWithConditionsAsync();
		}
		catch
		{
			return null;
		}
	}

	public async Task<Category?> GetByIdAsync(Guid id)
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

	public async Task<Category?> AddAsync(Category category)
	{
		try
		{
			await repository.AddAsync(category);
			return category;
		}
		catch
		{
			return null;
		}
	}

	public async Task<bool> UpdateAsync(Category category)
	{
		try
		{
			await repository.UpdateAsync(category);
			return true;
		}
		catch
		{
			return false;
		}
	}

	public async Task<bool> DeleteAsync(Category category)
	{
		try
		{
			await repository.RemoveAsync(category);
			return true;
		}
		catch
		{
			return false;
		}
	}
}