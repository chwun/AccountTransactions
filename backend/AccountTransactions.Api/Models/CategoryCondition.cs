namespace AccountTransactions.Api.Models;

public class CategoryCondition
{
	public Guid Id { get; set; }

	public CategoryConditionType Type { get; set; }

	public string Text { get; set; } = "";

	public Category Category { get; set; } = null!;

	public Guid CategoryGuid { get; set; }
}