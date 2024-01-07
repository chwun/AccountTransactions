using AccountTransactions.Api.Models;

namespace AccountTransactions.Api.Services;

public class CategoryConditionMatcher : ICategoryConditionMatcher
{
	public bool IsMatch(Transaction transaction, Category category)
	{
		foreach (CategoryCondition condition in category.Conditions)
		{
			switch (condition.Type)
			{
				case CategoryConditionType.SourceOrDestinationContains:
					if (transaction.SourceOrDestination.Contains(condition.Text, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}

					break;

				case CategoryConditionType.ReferenceContains:
					if (transaction.Reference.Contains(condition.Text, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}

					break;
			}
		}

		return false;
	}
}