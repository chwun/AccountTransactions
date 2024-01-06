using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models;

public class CategoryCondition
{
	public Guid Id { get; set; }

	public CategoryConditionType Type { get; set; }

	public string Text { get; set; } = "";

	public Category Category { get; set; } = null!;

	public Guid CategoryId { get; set; }

	public CategoryConditionDto ToDto() => new()
	{
		Id = Id,
		Type = Type,
		Text = Text
	};
}