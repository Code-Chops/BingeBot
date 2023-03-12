using BingeBot.Domain.Shows;

namespace BingeBot.Infrastructure.Databases.Shows;

public interface IShowRepo
{
	Task<List<Show>> GetShowsByFilterAsync(ShowFilter filter, CancellationToken cancellationToken);
	Task AddShowsAsync(IEnumerable<Show> shows, CancellationToken cancellationToken);
	Task AddShowAsync(Show show, CancellationToken cancellationToken);
	Task CreateOrUpdateShowAsync(Show show, CancellationToken cancellationToken);
	/// <summary>
	/// Returns the highest TVmaze show ID in the database. Returns 0 if no ID exists.
	/// </summary>
	Task<ShowTVmazeId> GetHighestTVmazeShowId();
	Task DeleteShowAsync(ShowId id, CancellationToken cancellationToken);
}