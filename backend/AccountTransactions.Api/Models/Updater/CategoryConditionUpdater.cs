using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models.Updater;

public class CategoryConditionUpdater : ICategoryConditionUpdater
{
	public void UpdateFromDto(CategoryCondition categoryCondition, CategoryConditionUpdateDto dto)
	{
		categoryCondition.Type = dto.Type ?? throw new InvalidDataException();
		categoryCondition.Text = dto.Text;
	}
}