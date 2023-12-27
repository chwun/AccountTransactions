using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Data.Repositories;

public class CategoryRepository : AsyncRepository<Category>, ICategoryRepository
{
	public CategoryRepository(AppDbContext context) : base(context)
	{
	}
}