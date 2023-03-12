namespace BingeBot.Infrastructure.Api.TVmaze.V1;

public interface IPersonEndpoint : IEndpoint
{
    /// <summary>
    /// Retrieves all main information for a given person.
    /// 
    /// https://www.tvmaze.com/api#person-main-information
    /// </summary>
    Task<PersonContract> GetPersonMainInformationAsync(int personId);
    
    /// <summary>
    /// Gets persons by using paging.
    /// 
    /// https://www.tvmaze.com/api#person-index
    /// </summary>
    /// <param name="page">Page of persons to get. The first page is 0.</param>
    /// <returns>The persons on the requested page of the index.</returns>
    /// <remarks>
    /// The pages are based on IDs. You cannot rely on getting a fixed amount of episodes because of deletions.
    /// You only know you encountered the end, if you get an empty list.
    /// </remarks>
    Task<IEnumerable<PersonContract>> GetPersonsAsync(int page);
}