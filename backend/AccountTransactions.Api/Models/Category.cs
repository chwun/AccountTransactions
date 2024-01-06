using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models;

public class Category
{
	public Guid Id { get; set; }

	public string Name { get; set; } = "";

	public TransactionType TransactionType { get; set; }

	public List<CategoryCondition> Conditions { get; set; } = [];

	public List<Transaction> Transactions { get; } = [];

	public CategoryDto ToDto()
	{
		return new()
		{
			Id = Id,
			Name = Name,
			TransactionType = TransactionType,
			Conditions = Conditions.ConvertAll(x => x.ToDto())
		};
	}
}