using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Models.Dtos;

public class TransactionFileUploadDto
{
	[Required]
	public IFormFile? File { get; set; }

	public TransactionImportFileType FileType { get; set; } = TransactionImportFileType.IngCsv;
}