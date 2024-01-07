using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Data.Repositories;

public interface ICategoryRepository : IAsyncRepository<Category>
{
	Task<IEnumerable<Category>?> GetAllWithConditionsAsync();
}