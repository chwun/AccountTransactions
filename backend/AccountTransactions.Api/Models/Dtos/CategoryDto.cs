namespace AccountTransactions.Api.Models.Dtos;

public class CategoryDto
{
	public Guid Id { get; set; }

	public string Name { get; set; } = "";

	public TransactionType TransactionType { get; set; }

	public List<CategoryConditionDto> Conditions { get; init; } = [];
}