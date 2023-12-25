namespace AccountTransactions.Api.Models.Dtos;

public class TransactionImportFileDto
{
	public Guid Id { get; set; }

	public string Filename { get; set; } = "";

	public TransactionImportFileType FileType { get; set; }

	public DateTime Timestamp { get; set; }
}