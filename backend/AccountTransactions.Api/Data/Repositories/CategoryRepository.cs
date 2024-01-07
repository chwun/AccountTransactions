using AccountTransactions.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactions.Api.Data.Repositories;

public class CategoryRepository : AsyncRepository<Category>, ICategoryRepository
{
	public CategoryRepository(AppDbContext context) : base(context)
	{
	}

	public override async Task<Category?> GetAsync(Guid id)
	{
		return await DatabaseContext.Set<Category>().Include(c => c.Conditions).FirstOrDefaultAsync(x => x.Id.Equals(id));
	}

	public async Task<IEnumerable<Category>?> GetAllWithConditionsAsync()
	{
		return await DatabaseContext.Set<Category>().Include(c => c.Conditions).ToListAsync();
	}
}