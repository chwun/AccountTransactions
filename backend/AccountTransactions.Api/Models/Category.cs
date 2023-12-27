namespace AccountTransactions.Api.Models;

public class Category
{
	public Guid Id { get; set; }

	public string Name { get; set; } = "";

	public TransactionType TransactionType { get; set; }

	public List<CategoryCondition> Conditions { get; } = [];

	public List<Transaction> Transactions { get; } = [];
}