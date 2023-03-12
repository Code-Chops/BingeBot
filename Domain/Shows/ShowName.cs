namespace BingeBot.Domain.Shows;

/// <summary>
/// The name of a show.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 1, maximumLength: 100, // This should be enough: ChatGPT says the longest television show name has 63 characters. The longest movie name has 82 characters. 
	useRegex: false, 
	StringFormat.Default, StringComparison.InvariantCultureIgnoreCase,
	forbidParameterlessConstruction: false)]
public readonly partial record struct ShowName
{
	public readonly string ErrorCode_BingeBot_ShowName_Null				= nameof(ErrorCode_BingeBot_ShowName_Null);
	public readonly string ErrorCode_BingeBot_ShowName_LengthOutOfRange	= nameof(ErrorCode_BingeBot_ShowName_LengthOutOfRange);
}