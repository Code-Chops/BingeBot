using BingeBot.Infrastructure.Api.TVmaze.Http;
using Flurl;

namespace BingeBot.Infrastructure.Api.TVmaze.V1;

public sealed class PersonEndpoint : IPersonEndpoint
{
    public string RelativeUrl => "people";

    private ITVmazeHttpClient TVmazeClient { get; }

    public PersonEndpoint(ITVmazeHttpClient tVmazeClient)
    {
        this.TVmazeClient = tVmazeClient;
    }
    
    /// <inheritdoc />
    public Task<IEnumerable<PersonContract>> GetPersonsAsync(int page)
    {
        if (page < 0)
            throw new ArgumentOutOfRangeException(nameof(page));

        var url = this.RelativeUrl.SetQueryParam(TVmazeQueryParameters.Page, page);
        return this.TVmazeClient.GetListAsync<PersonContract>(url);
    }
    
    /// <inheritdoc />
    public Task<PersonContract> GetPersonMainInformationAsync(int personId)
    {
        if (personId <= 0)
            throw new ArgumentOutOfRangeException(nameof(personId));

        return this.TVmazeClient.GetSingleAsync<PersonContract>($"{this.RelativeUrl}/{personId}");
    }
}