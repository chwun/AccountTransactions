using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models.Updater;

public class CategoryUpdater : ICategoryUpdater
{
	private readonly ICategoryConditionUpdater conditionUpdater;

	public CategoryUpdater(ICategoryConditionUpdater conditionUpdater)
	{
		this.conditionUpdater = conditionUpdater;
	}

	public void UpdateFromDto(Category category, CategoryUpdateDto dto)
	{
		category.Name = dto.Name;
		category.TransactionType = dto.TransactionType;
		category.Conditions = [];

		foreach (CategoryConditionUpdateDto conditionDto in dto.Conditions)
		{
			CategoryCondition categoryCondition = new();
			conditionUpdater.UpdateFromDto(categoryCondition, conditionDto);
			category.Conditions.Add(categoryCondition);
		}
	}
}