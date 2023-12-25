using Microsoft.EntityFrameworkCore;

namespace AccountTransactions.Api.Data;

/// <summary>
/// Generic abstract base class for an async DB repository.
/// </summary>
/// <typeparam name="TModel">model type</typeparam>
public abstract class AsyncRepository<TModel> : IAsyncRepository<TModel> where TModel : class
{
	protected readonly DbContext DatabaseContext;


	public AsyncRepository(DbContext context)
	{
		DatabaseContext = context;
	}

	/// <summary>
	/// Gets a single object by id
	/// </summary>
	/// <param name="id">id</param>
	/// <returns>object with given id (if found)</returns>
	public virtual async Task<TModel?> GetAsync(Guid id) => await DatabaseContext.Set<TModel>().FindAsync(id);

	/// <summary>
	/// Gets all objects
	/// </summary>
	/// <returns>list of objects</returns>
	public async Task<IEnumerable<TModel>> GetAllAsync() => await DatabaseContext.Set<TModel>().ToListAsync();

	/// <summary>
	/// Gets all objects without change tracking
	/// </summary>
	/// <returns>list of objects without change tracking</returns>
	public virtual async Task<IEnumerable<TModel>> GetAllAsNoTrackingAsync() => await DatabaseContext.Set<TModel>().AsNoTracking().ToListAsync();

	/// <summary>
	/// Adds the given object
	/// </summary>
	/// <param name="entity">object to add</param>
	public virtual async Task AddAsync(TModel entity)
	{
		await DatabaseContext.Set<TModel>().AddAsync(entity);
		await DatabaseContext.SaveChangesAsync();
	}

	/// <summary>
	/// Removes the given object
	/// </summary>
	/// <param name="entity">object to remove</param>
	public virtual async Task RemoveAsync(TModel entity)
	{
		DatabaseContext.Set<TModel>().Remove(entity);
		await DatabaseContext.SaveChangesAsync();
	}

	/// <summary>
	/// Updates the given object
	/// </summary>
	/// <param name="entity">object to update</param>
	public virtual async Task UpdateAsync(TModel entity)
	{
		DatabaseContext.Set<TModel>().Update(entity);
		await DatabaseContext.SaveChangesAsync();
	}
}