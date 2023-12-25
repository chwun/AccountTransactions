using AccountTransactions.Api.Helpers;
using AccountTransactions.Api.Models;
using AccountTransactions.Api.Models.Dtos;
using AccountTransactions.Api.Services.DataAccess;

namespace AccountTransactions.Api.Services;

public class TransactionFileImport : ITransactionFileImport
{
	private readonly IIngCsvImport csvImport;
	private readonly IDateTimeProvider dateTimeProvider;
	private readonly IBarclaysExcelImport excelImport;
	private readonly ITransactionImportFileAccess importFileAccess;

	public TransactionFileImport(ITransactionImportFileAccess importFileAccess, IIngCsvImport csvImport, IBarclaysExcelImport excelImport,
		IDateTimeProvider dateTimeProvider)
	{
		this.importFileAccess = importFileAccess;
		this.csvImport = csvImport;
		this.excelImport = excelImport;
		this.dateTimeProvider = dateTimeProvider;
	}

	public async Task<TransactionImportFile?> ImportFile(TransactionFileUploadDto importData)
	{
		if (importData.File is null)
		{
			return null;
		}

		try
		{
			TransactionImportFile importFile = new()
			{
				Filename = importData.File.FileName,
				FileType = importData.FileType,
				Timestamp = dateTimeProvider.UtcNow
			};

			using Stream stream = importData.File.OpenReadStream();
			var transactions = importData.FileType switch
			{
				TransactionImportFileType.IngCsv => await csvImport.ReadTransactionsFromCsvFileAsync(stream),
				TransactionImportFileType.BarclaysExcel => await excelImport.ReadTransactionsFromExcelFileAsync(stream),
				_ => throw new NotImplementedException()
			};

			importFile.Transactions.AddRange(transactions);
			await importFileAccess.AddAsync(importFile);

			return importFile;
		}
		catch
		{
			return null;
		}
	}
}