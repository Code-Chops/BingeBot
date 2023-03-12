using System.Net;
using Flurl.Http;
using Polly;

namespace BingeBot.Infrastructure.Api.TVmaze.Configuration;

/// <summary>
/// Will retry the request multiple times (with a delay), if a rate limit is encountered.
/// </summary>
public sealed class RetryRateLimitingStrategy : IRateLimitingStrategy
{
    private const int DefaultRetries = 5;
    private static readonly TimeSpan DefaultRetryInterval = TimeSpan.FromSeconds(3);
    
    private AsyncPolicy<IFlurlResponse> Policy { get; }

    /// <summary>
    /// Creates an instance with reasonable default values for the rate limits described by the API.
    /// </summary>
    public RetryRateLimitingStrategy()
        : this(DefaultRetries, DefaultRetryInterval)
    {
    }

    /// <summary>
    /// Creates an instance that uses the provided configuration values for retrying.
    /// </summary>
    public RetryRateLimitingStrategy(int retries, TimeSpan retryInterval)
    {
        this.Policy = Polly.Policy
            .HandleResult<IFlurlResponse>(response => response.StatusCode == (int)HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(retries, _ => retryInterval);
    }

    /// <inheritdoc />
    public Task<IFlurlResponse> ExecuteAsync(Func<Task<IFlurlResponse>> action)
    {
        return this.Policy.ExecuteAsync(action);
    }
}