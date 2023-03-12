using BingeBot.Infrastructure.Api.TVmaze.Http;
using Flurl;

namespace BingeBot.Infrastructure.Api.TVmaze.V1;

public sealed class ShowEndpoint : IShowEndpoint
{
    public string RelativeUrl => "shows";

    private ITVmazeHttpClient TVmazeClient { get; }

    public ShowEndpoint(ITVmazeHttpClient tVmazeClient)
    {
        this.TVmazeClient = tVmazeClient;
    }
    
    /// <inheritdoc />
    public async Task<IEnumerable<ShowContract>> GetShowsAsync(int page)
    {
        if (page < 0)
            throw new ArgumentOutOfRangeException(nameof(page));
    
        return await this.TVmazeClient.GetListAsync<ShowContract>(this.RelativeUrl.SetQueryParam(TVmazeQueryParameters.Page, page));
    }
    
    /// <inheritdoc />
    public async Task<ShowContract> GetShowMainInformationAsync(int showId)
    {
        if (showId <= 0)
            throw new ArgumentOutOfRangeException(nameof(showId));

        return await this.TVmazeClient.GetSingleAsync<ShowContract>($"{this.RelativeUrl}/{showId}");
    }
}