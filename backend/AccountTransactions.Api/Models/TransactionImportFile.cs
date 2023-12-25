using AccountTransactions.Api.Models.Dtos;

namespace AccountTransactions.Api.Models;

public class TransactionImportFile
{
	public Guid Id { get; set; }

	public string Filename { get; set; } = "";

	public TransactionImportFileType FileType { get; set; }

	public DateTime Timestamp { get; set; }

	public List<Transaction> Transactions { get; } = new List<Transaction>();

	public TransactionImportFileDto ToDto()
	{
		return new()
		{
			Id = Id,
			Filename = Filename,
			FileType = FileType,
			Timestamp = Timestamp
		};
	}
}
