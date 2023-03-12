using BingeBot.Contracts.BingeBot.V1.Shows;
using BingeBot.Domain.Shows;

namespace BingeBot.Application;

/// <summary>
/// Contains CRUD methods and other actions on a show or a list of shows.
/// </summary>
public interface IShowApplicationService
{
	Task CreateShowAsync(CreateShowRequest request, CancellationToken cancellationToken);
	Task CreateShowsAsync(IEnumerable<Show> shows, CancellationToken cancellationToken);
	Task<IEnumerable<GetShowResponse>> GetFilteredShowsAsync(GetShowsRequest request, CancellationToken cancellationToken);
	Task CreateOrUpdateShowAsync(int? id, UpdateShowRequest request, CancellationToken cancellationToken);
	Task DeleteShowAsync(int showId, CancellationToken cancellationToken);
	
	/// <summary>
	/// Returns the highest TVmaze show ID that is stored.
	/// </summary>
	Task<ShowTVmazeId> GetHighestTVmazeShowId();
}