namespace AccountTransactions.Api.Helpers;

/// <summary>
/// Interface for accessing DateTime
/// </summary>
public interface IDateTimeProvider
{
	/// <summary>
	/// Gets current UTC timestamp
	/// </summary>
	DateTime UtcNow { get; }
}