using System.ComponentModel.DataAnnotations;

namespace AccountTransactions.Api.Helpers;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EnumValueDefinedAttribute : ValidationAttribute
{
	private const string errorMessage = "The {0} field must be an valid enum value";
	private readonly Type enumType;

	public EnumValueDefinedAttribute(Type enumType) : base(errorMessage)
	{
		this.enumType = enumType;
	}

	public override bool IsValid(object? value)
	{
		if (value is null)
		{
			return false;
		}

		return Enum.IsDefined(enumType, value);
	}
}