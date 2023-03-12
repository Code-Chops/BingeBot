namespace BingeBot.Domain.Persons;

/// <summary>
/// The name of a person.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 1, maximumLength: 50, 
	useRegex: false, 
	StringFormat.Default, StringComparison.InvariantCultureIgnoreCase,
	forbidParameterlessConstruction: false)]
public readonly partial record struct PersonName
{
	public readonly string ErrorCode_BingeBot_PersonName_Null				= nameof(ErrorCode_BingeBot_PersonName_Null);
	public readonly string ErrorCode_BingeBot_PersonName_LengthOutOfRange	= nameof(ErrorCode_BingeBot_PersonName_LengthOutOfRange);
}