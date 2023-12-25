using AccountTransactions.Api.Models;
using System.Globalization;

namespace AccountTransactions.Api.Services;

public class IngCsvImport : IIngCsvImport
{
	private readonly CultureInfo cultureDe = new("de-de");

	public async Task<List<Transaction>> ReadTransactionsFromCsvFileAsync(Stream stream)
	{
		using StreamReader reader = new StreamReader(stream);

		List<string> lines = [];

		while (!reader.EndOfStream)
		{
			string? line = await reader.ReadLineAsync();
			if (line is null)
			{
				continue;
			}

			lines.Add(line);
		}

		return ParseCsvLines(lines);
	}

	private List<Transaction> ParseCsvLines(List<string> lines)
	{
		List<Transaction> transactions = [];

		foreach (string line in lines.SkipWhile(x => !x.StartsWith("Buchung")).Skip(1))
		{
			string[] parts = line.Split(";");

			decimal amount = decimal.Parse(parts[7], cultureDe.NumberFormat);

			Transaction transaction = new()
			{
				Reference = parts[4],
				Amount = amount,
				SourceOrDestination = parts[2],
				Type = amount < 0 ? TransactionType.Expense : TransactionType.Revenue,
				Timestamp = DateTime.Parse(parts[1], cultureDe.DateTimeFormat)
			};

			transactions.Add(transaction);
		}

		return transactions;
	}
}