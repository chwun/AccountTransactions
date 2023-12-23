using System;
using AccountTransactions.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountTransactions.Api.Data;

/// <summary>
/// App database context
/// </summary>
public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	/// <summary>
	/// Set of transactions
	/// </summary>
	public DbSet<Transaction> Transactions { get; set; } = null!;
}