using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models.Updater;

public interface ICategoryUpdater
{
	void UpdateFromDto(Category category, CategoryUpdateDto dto);
}