using System.Globalization;
using AccountTransactions.Api.Models;
using MiniExcelLibs;

namespace AccountTransactions.Api.Services;

public class BarclaysExcelImport : IBarclaysExcelImport
{
	private readonly CultureInfo cultureDe = new("de-de");

	public async Task<List<Transaction>> ReadTransactionsFromExcelFileAsync(Stream stream)
	{
		List<Transaction> transactions = [];

		foreach (var row in (await stream.QueryAsync()).SkipWhile(x => x.A != "Referenznummer").Skip(1))
		{
			decimal amount = decimal.Parse(row.D.TrimEnd('€'), cultureDe.NumberFormat);

			Transaction transaction = new()
			{
				Reference = "",
				Amount = amount,
				SourceOrDestination = row.E,
				Type = amount < 0 ? TransactionType.Expense : TransactionType.Revenue,
				Timestamp = DateTime.Parse(row.B, cultureDe.DateTimeFormat)
			};

			transactions.Add(transaction);
		}

		return transactions;
	}
}
