namespace BingeBot.Domain.Shows;

/// <summary>
/// The summary of a show.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 0, maximumLength: Int32.MaxValue,
	useRegex: false, 
	StringFormat.Default, StringComparison.InvariantCultureIgnoreCase,
	forbidParameterlessConstruction: false, 
	valueIsNullable: true)]
public readonly partial record struct ShowSummary
{
	public readonly string ErrorCode_BingeBot_ShowSummary_Null				= nameof(ErrorCode_BingeBot_ShowSummary_Null);
	public readonly string ErrorCode_BingeBot_ShowSummary_LengthOutOfRange	= nameof(ErrorCode_BingeBot_ShowSummary_LengthOutOfRange);
}