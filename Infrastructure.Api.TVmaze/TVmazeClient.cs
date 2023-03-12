using BingeBot.Infrastructure.Api.TVmaze.V1;

namespace BingeBot.Infrastructure.Api.TVmaze;

/// <inheritdoc />
public sealed class TVmazeClient : ITVmazeClient
{
    public IShowEndpoint ShowEndpoint { get; }
    public IPersonEndpoint PersonEndpoint { get; }
    
    public TVmazeClient(IShowEndpoint showEndpoint, IPersonEndpoint personEndpoint)
    {
        this.ShowEndpoint = showEndpoint ?? throw new ArgumentNullException(nameof(showEndpoint));
        this.PersonEndpoint = personEndpoint ?? throw new ArgumentNullException(nameof(personEndpoint));
    }
}