using BingeBot.Domain.Shows;

namespace BingeBot.Infrastructure.Databases.Shows;

public sealed record HighestTVmazeIdFilter : ShowFilter
{
	public HighestTVmazeIdFilter() 
		: base(page: 0, size: null)
	{
	}
	
	public override IQueryable<Show> ApplyFilter(IQueryable<Show> shows)
	{
		return shows.Where(show => show.TVmazeId != null);
	}
}