using BingeBot.Domain.ShowGenres;

namespace BingeBot.Domain.Shows;

/// <summary>
/// A collection of genres that belong to a show.
/// </summary>
[GenerateListValueObject<ShowGenre>]
public readonly partial record struct ShowGenres
{
	public readonly string ErrorCode_BingeBot_ShowGenres_Null = nameof(ErrorCode_BingeBot_ShowGenres_Null);
}