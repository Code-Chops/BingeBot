using BingeBot.Domain.Shows;

namespace BingeBot.Domain.ShowGenres;

[GenerateIdentity<int>(nameof(ShowGenreId))]
public sealed class ShowGenre : Entity<ShowGenreId>
{
	public override string ToString() => this.Name.ToString();
	
	public ShowId ShowId { get; }
	public ShowGenreName Name { get; }

	public Show Show { get; } = default!;
	
	public ShowGenre(ShowGenreName name)
	{
		this.Name = name;
	}

	public ShowGenre(ShowId showId, ShowGenreName name)
	{
		this.ShowId = showId;
		this.Name = name;
	}

	#region Entity Framework Constructor
	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.    
	[Obsolete("Reconstitution only.")]
	// ReSharper disable once UnusedMember.Local
	private ShowGenre()
	{
	}
	#pragma warning restore
	#endregion
}
