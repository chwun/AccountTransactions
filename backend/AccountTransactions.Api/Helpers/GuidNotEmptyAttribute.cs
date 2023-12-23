using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Helpers;

/// <summary>
/// Attribute for validating that an guid is not empty
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class GuidNotEmptyAttribute : ValidationAttribute
{
	public const string DefaultErrorMessage = "The {0} field must not be an empty guid";

	public GuidNotEmptyAttribute() : base(DefaultErrorMessage) { }

	public override bool IsValid(object? value)
	{
		if (value is null)
		{
			return true;
		}

		switch (value)
		{
			case Guid guid:
				return guid != Guid.Empty;
			default:
				return true;
		}
	}
}
