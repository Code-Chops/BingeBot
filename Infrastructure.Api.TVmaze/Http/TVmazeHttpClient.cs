using System.Net;
using BingeBot.Infrastructure.Api.TVmaze.Configuration;
using BingeBot.Infrastructure.Api.TVmaze.Exceptions;
using Flurl.Http;

namespace BingeBot.Infrastructure.Api.TVmaze.Http;

public class TVmazeHttpClient : ITVmazeHttpClient
{
    private IFlurlClient FlurlClient { get; }
    private IRateLimitingStrategy RateLimitingStrategy { get; }

    public TVmazeHttpClient(IFlurlClient flurlClient, IRateLimitingStrategy rateLimitingStrategy)
    {
        this.FlurlClient = flurlClient;
        this.RateLimitingStrategy = rateLimitingStrategy;
    }

    public async Task<IEnumerable<TElement>> GetListAsync<TElement>(string url)
    {
        return await this.GetAsync(url, Array.Empty<TElement>);
    }

    public async Task<T> GetSingleAsync<T>(string url)
    {
        return await this.GetAsync<T>(url, () => default!);
    }

    private async Task<TReturn> GetAsync<TReturn>(string url, Func<TReturn> emptyValueGetter)
    {
        using var httpResponse = await this.RateLimitingStrategy.ExecuteAsync(() => this.FlurlClient.Request(url).GetAsync());

        switch (httpResponse.StatusCode)
        {
            case (int)HttpStatusCode.OK:
                return await httpResponse.GetJsonAsync<TReturn>();
            
            case (int)HttpStatusCode.TooManyRequests:
                throw new TooManyRequestsException();
                
            case (int)HttpStatusCode.NotFound:
                return emptyValueGetter();

            default:
                throw new UnexpectedResponseStatusException($"Unexpected error code {httpResponse.StatusCode.ToString()} was returned by TVmaze.", (HttpStatusCode)httpResponse.StatusCode);
        }
    }
}