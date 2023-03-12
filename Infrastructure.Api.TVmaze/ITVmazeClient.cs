using BingeBot.Infrastructure.Api.TVmaze.V1;

namespace BingeBot.Infrastructure.Api.TVmaze;

/// <summary>
/// Communicates with TVmaze. Only <see cref="ShowEndpoint"/> and <see cref="PersonEndpoint"/> are implemented. This could be expanded....
/// </summary>
public interface ITVmazeClient
{
	IShowEndpoint ShowEndpoint { get; }
	IPersonEndpoint PersonEndpoint { get; }
}