namespace BingeBot.Infrastructure.Api.TVmaze.V1;

public interface IShowEndpoint : IEndpoint
{
    /// <summary>
    /// Retrieves all main information for a given show.
    /// 
    /// https://www.tvmaze.com/api#show-main-information
    /// </summary>
    Task<ShowContract> GetShowMainInformationAsync(int showId);
    
    /// <summary>
    /// Gets shows by using paging.
    /// 
    /// https://www.tvmaze.com/api#show-index
    /// </summary>
    /// <param name="page">Page of shows to get. The first page is 0.</param>
    /// <returns>The shows on the requested page of the index.</returns>
    /// <remarks>
    /// The pages are based on IDs. You cannot rely on getting a fixed amount of episodes because of deletions.
    /// You only know you encountered the end, if you get an empty list.
    /// </remarks>
    Task<IEnumerable<ShowContract>> GetShowsAsync(int page);
}