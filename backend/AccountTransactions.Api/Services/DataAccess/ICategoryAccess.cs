using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services.DataAccess;

public interface ICategoryAccess
{
	Task<IEnumerable<Category>?> GetAllAsync();
}