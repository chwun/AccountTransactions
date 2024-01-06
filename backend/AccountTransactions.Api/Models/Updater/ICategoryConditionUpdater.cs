using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models.Updater;

public interface ICategoryConditionUpdater
{
	void UpdateFromDto(CategoryCondition categoryCondition, CategoryConditionUpdateDto dto);
}