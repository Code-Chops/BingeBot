namespace BingeBot.Domain.ShowGenres;

/// <summary>
/// The name of a genre.
/// </summary>
[GenerateStringValueObject(
	minimumLength: 1, maximumLength: 50,
	useRegex: false, 
	StringFormat.Default, StringComparison.InvariantCultureIgnoreCase,
	forbidParameterlessConstruction: false)]
// Can't be a struct because of EF.
public readonly partial struct ShowGenreName
{
	public readonly string ErrorCode_BingeBot_ShowGenreName_Null			 = nameof(ErrorCode_BingeBot_ShowGenreName_Null);
	public readonly string ErrorCode_BingeBot_ShowGenreName_LengthOutOfRange = nameof(ErrorCode_BingeBot_ShowGenreName_LengthOutOfRange);
}