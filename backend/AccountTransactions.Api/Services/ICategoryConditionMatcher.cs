using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services;

public interface ICategoryConditionMatcher
{
	bool IsMatch(Transaction transaction, Category category);
}