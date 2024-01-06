using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public interface ICategoryAccess
{
	Task<IEnumerable<Category>?> GetAllAsync();

	Task<Category?> GetByIdAsync(Guid id);

	Task<Category?> AddAsync(Category category);

	Task<bool> UpdateAsync(Category category);

	Task<bool> DeleteAsync(Category category);
}