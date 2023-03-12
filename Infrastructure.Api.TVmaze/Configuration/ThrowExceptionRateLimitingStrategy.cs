using System.Net;
using BingeBot.Infrastructure.Api.TVmaze.Exceptions;
using Flurl.Http;

namespace BingeBot.Infrastructure.Api.TVmaze.Configuration;

/// <summary>
/// Will throw an <see cref="TooManyRequestsException"/> if a rate limit is encountered.
/// </summary>
public sealed class ThrowExceptionRateLimitingStrategy : IRateLimitingStrategy
{
    /// <inheritdoc />
    /// <exception cref="TooManyRequestsException"></exception>
    public async Task<IFlurlResponse> ExecuteAsync(Func<Task<IFlurlResponse>> action)
    {
        var response = await action();

        if (response.StatusCode == (int)HttpStatusCode.TooManyRequests)
            throw new TooManyRequestsException();

        return response;
    }
}