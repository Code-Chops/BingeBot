using Flurl.Http;

namespace BingeBot.Infrastructure.Api.TVmaze.Configuration;

/// <summary>
/// Defines how rate limiting is handled.
/// </summary>
public interface IRateLimitingStrategy
{
    /// <summary>
    /// Handler for HTTPRequests.
    /// </summary>
    /// <param name="action">The function that sends the HTTP Request.</param>
    /// <returns>The response from the TVmaze API.</returns>
    Task<IFlurlResponse> ExecuteAsync(Func<Task<IFlurlResponse>> action);
}