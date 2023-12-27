using AccountTransactions.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactions.Api.Data;

/// <summary>
/// App database context
/// </summary>
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	/// <summary>
	/// Set of transactions
	/// </summary>
	public DbSet<Transaction> Transactions { get; set; } = null!;

	/// <summary>
	/// Set of import files
	/// </summary>
	public DbSet<TransactionImportFile> ImportFiles { get; set; } = null!;

	/// <summary>
	/// Set of categories
	/// </summary>
	public DbSet<Category> Categories { get; set; } = null!;
}