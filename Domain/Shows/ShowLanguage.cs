namespace BingeBot.Domain.Shows;

/// <summary>
/// The language of a show. I would have preferred that this is an ISO code, but it's not.
/// <see cref="System.Globalization.CultureInfo"/> has a better way to compare languages, but it has its drawbacks when you try to get it from a language string.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 0, maximumLength: 50, 
	useRegex: false, 
	StringFormat.Default, StringComparison.InvariantCultureIgnoreCase, 
	useValidationExceptions: true,
	forbidParameterlessConstruction: false, 
	valueIsNullable: true)]
public readonly partial record struct ShowLanguage
{
	public readonly string ErrorCode_BingeBot_ShowLanguage_Null				= nameof(ErrorCode_BingeBot_ShowLanguage_Null);
	public readonly string ErrorCode_BingeBot_ShowLanguage_LengthOutOfRange = nameof(ErrorCode_BingeBot_ShowLanguage_LengthOutOfRange);
}