namespace BingeBot.Infrastructure.Api.TVmaze;

public interface IEndpoint
{
	/// <summary>
	/// The relative URL of the endpoint.
	/// </summary>
	string RelativeUrl { get; }
}