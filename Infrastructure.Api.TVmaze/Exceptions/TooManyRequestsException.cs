using System.Net;

namespace BingeBot.Infrastructure.Api.TVmaze.Exceptions;

/// <summary>
/// Will be thrown when the 
/// </summary>
public sealed class TooManyRequestsException : HttpRequestException
{
    public TooManyRequestsException(string? message = null, Exception? innerException = null)
        : base(
            message: message ?? $"Too many requests to the TVmaze API: {HttpStatusCode.TooManyRequests}. This should not happen.", 
            inner: innerException, 
            statusCode: HttpStatusCode.TooManyRequests)
    {
    }
}