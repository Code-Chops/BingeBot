using BingeBot.Domain.ShowGenres;

namespace BingeBot.Domain.Shows;

/// <summary>
/// A show.
/// </summary>
[GenerateIdentity<int>(nameof(ShowId))]
public sealed class Show : Entity<ShowId>
{
	/// <summary>
	/// An inserted show is not linked to (or even exist in) the TVmaze database.
	/// </summary>
	public ShowTVmazeId? TVmazeId { get; }
	
	public ShowName Name { get; }

	public List<ShowGenre> Genres { get; } = new();
	public ShowLanguage Language { get; }
	public ShowPremieredDate PremieredDate { get; }
	public ShowSummary Summary { get; }

	/* Could be expanded with more properties... */
	
	/// <summary>
	/// Used by adapters.
	/// </summary>
	internal Show(ShowId id, ShowTVmazeId? tVmazeId, ShowName name, IEnumerable<ShowGenre> genres, ShowLanguage language, ShowPremieredDate premieredDate, ShowSummary summary)
	{
		this.Id = id;
		this.TVmazeId = tVmazeId;
		this.Name = name;
		this.Genres = genres.ToList();
		this.Language = language;
		this.PremieredDate = premieredDate;
		this.Summary = summary;
	}
	
	/// <summary>
	/// Used for creating a non-existing entity in the database. 
	/// </summary>
	public Show(ShowTVmazeId? tVmazeId, ShowName name, IEnumerable<ShowGenre> genres, ShowLanguage language, ShowPremieredDate premieredDate, ShowSummary summary)
	{
		this.Id = default;
		this.TVmazeId = tVmazeId;
		this.Name = name;
		this.Genres = genres.ToList();
		this.Language = language;
		this.PremieredDate = premieredDate;
		this.Summary = summary;
	}

	#region Entity Framework Constructor
	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.    
	[Obsolete("Reconstitution only.")]
	// ReSharper disable once UnusedMember.Local
	private Show()
	{
	}
	#pragma warning restore
    #endregion
}