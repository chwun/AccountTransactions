namespace AccountTransactions.Api.Helpers;

/// <summary>
/// Class for accessing DateTime
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
	/// <summary>
	/// Gets current UTC timestamp
	/// </summary>
	public DateTime UtcNow => DateTime.UtcNow;
}