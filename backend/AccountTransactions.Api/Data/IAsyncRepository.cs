namespace AccountTransactions.Api.Data;

/// <summary>
/// Generic interface for an async DB repository
/// </summary>
/// <typeparam name="TModel">model type</typeparam>
public interface IAsyncRepository<TModel> where TModel : class
{
	/// <summary>
	/// Gets a single object by id
	/// </summary>
	/// <param name="id">id</param>
	/// <returns>object with given id (if found)</returns>
	Task<TModel?> GetAsync(Guid id);

	/// <summary>
	/// Gets all objects
	/// </summary>
	/// <returns>list of objects</returns>
	Task<IEnumerable<TModel>> GetAllAsync();

	/// <summary>
	/// Gets all objects without change tracking
	/// </summary>
	/// <returns>list of objects without change tracking</returns>
	Task<IEnumerable<TModel>> GetAllAsNoTrackingAsync();

	/// <summary>
	/// Adds the given object
	/// </summary>
	/// <param name="entity">object to add</param>
	Task AddAsync(TModel entity);

	/// <summary>
	/// Removes the given object
	/// </summary>
	/// <param name="entity">object to remove</param>
	Task RemoveAsync(TModel entity);

	/// <summary>
	/// Updates the given object
	/// </summary>
	/// <param name="entity">object to update</param>
	Task UpdateAsync(TModel entity);
}