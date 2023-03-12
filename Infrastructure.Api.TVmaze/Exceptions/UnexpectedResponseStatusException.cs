using System.Net;

namespace BingeBot.Infrastructure.Api.TVmaze.Exceptions;

/// <summary>
/// Is thrown when an other HTTP status code is returned by the TVmaze API then the expected ones at <see cref="BingeBot.Infrastructure.Api.TVmaze.Http.TVmazeHttpClient.GetAsync{TReturn}"/>
/// </summary>
public sealed class UnexpectedResponseStatusException : HttpRequestException
{
    public UnexpectedResponseStatusException(HttpStatusCode statusCode)
        : this($"Received unexpected response status: {statusCode}.", statusCode)
    {
    }

    public UnexpectedResponseStatusException(string message, HttpStatusCode statusCode, Exception? innerException = null)
        : base(message, innerException, statusCode)
    {
    }
}