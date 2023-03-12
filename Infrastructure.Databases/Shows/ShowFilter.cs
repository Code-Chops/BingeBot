using BingeBot.Domain.Shows;

namespace BingeBot.Infrastructure.Databases.Shows;

/// <summary>
/// Filtering of <see cref="BingeBot.Domain.Shows.Show"/> queries.
/// </summary>
public record ShowFilter : PagingFilter<Show>, IFilterWithPaging<Show>
{
	public ShowId? Id { get; init; }
	public ShowTVmazeId? TVmazeId { get; init; }
	public ShowName? Name { get; init; }
	public ShowPremieredDate? PremieredDate { get; init; }

	/// <summary>
	/// Filtering of show queries.
	/// </summary>
	public ShowFilter(int page, int? size)
		: base(page, size)
	{
	}
	
	public virtual IQueryable<Show> ApplyFilter(IQueryable<Show> shows)
	{
		if (this.Id is not null)
			shows = shows.Where(show => show.Id == this.Id);

		if (this.TVmazeId is not null)
			shows = shows.Where(show => show.TVmazeId == this.TVmazeId);

		if (this.Name is not null)
			shows = shows.Where(show => show.Name == this.Name);
		
		if (this.PremieredDate is not null)
			shows = shows.Where(show => show.PremieredDate == this.PremieredDate);

		return shows;
	}
}