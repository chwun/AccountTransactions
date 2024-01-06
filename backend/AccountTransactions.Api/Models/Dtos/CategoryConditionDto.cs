namespace AccountTransactions.Api.Models.Dtos;

public class CategoryConditionDto
{
	public Guid Id { get; set; }

	public CategoryConditionType Type { get; set; }

	public string Text { get; set; } = "";
}