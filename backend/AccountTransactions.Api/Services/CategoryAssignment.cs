using AccountTransactions.Api.Models;
using AccountTransactions.Api.Services.DataAccess;

namespace AccountTransactions.Api.Services;

public class CategoryAssignment : ICategoryAssignment
{
	private readonly ICategoryAccess categoryAccess;
	private readonly ICategoryConditionMatcher categoryConditionMatcher;
	private readonly ITransactionAccess transactionAccess;

	public CategoryAssignment(ITransactionAccess transactionAccess, ICategoryAccess categoryAccess,
		ICategoryConditionMatcher categoryConditionMatcher)
	{
		this.transactionAccess = transactionAccess;
		this.categoryAccess = categoryAccess;
		this.categoryConditionMatcher = categoryConditionMatcher;
	}

	public async Task<bool> AssignTransactionsOfImportFile(Guid importFileId)
	{
		var transactions = await transactionAccess.GetAllByImportFileAsync(importFileId);
		if (transactions is null)
		{
			return false;
		}

		var categories = await categoryAccess.GetAllWithConditionsAsync();
		if (categories is null)
		{
			return false;
		}

		foreach (Transaction transaction in transactions)
		{
			Category? matchingCategory = FindMatchingCategory(transaction, categories);
			if (matchingCategory != null)
			{
				transaction.Category = matchingCategory;
				await transactionAccess.UpdateAsync(transaction);
			}
		}

		return true;
	}

	private Category? FindMatchingCategory(Transaction transaction, IEnumerable<Category> categories)
	{
		foreach (Category category in categories)
		{
			if (categoryConditionMatcher.IsMatch(transaction, category))
			{
				return category;
			}
		}

		return null;
	}
}