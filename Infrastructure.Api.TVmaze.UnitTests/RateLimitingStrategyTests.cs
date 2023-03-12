using BingeBot.Infrastructure.Api.TVmaze.Exceptions;
using BingeBot.Infrastructure.Api.TVmaze.Http;

namespace BingeBot.Infrastructure.Api.TVmaze.UnitTests;

public sealed class RateLimitingStrategyTests : IDisposable
{
    private IFlurlClient FlurlClient { get; }
    private HttpTest HttpTest { get; } = new();
    
    public RateLimitingStrategyTests(IFlurlClient flurlClient)
    {
        this.FlurlClient = flurlClient;
        this.HttpTest.RespondWith(body: "", (int)HttpStatusCode.TooManyRequests);
    }
        
    /// <summary>
    /// Checks if <see cref="ThrowExceptionRateLimitingStrategy"/> throws and <see cref="TooManyRequestsException"/> when the rate limit has been exceeded.
    /// </summary>
    [Fact]
    public async void ThrowExceptionRateLimitingStrategy()
    {
        // arrange
        var strategy = new ThrowExceptionRateLimitingStrategy();
        var showEndpoint = new ShowEndpoint(new TVmazeHttpClient(this.FlurlClient, strategy));
        var beforeCount = this.HttpTest.CallLog.Count;

        // act
        var action = async () => await showEndpoint.GetShowMainInformationAsync(1);

        // assert
        await action.Should().ThrowAsync<TooManyRequestsException>();
        this.HttpTest.CallLog.Count.Should().Be(beforeCount + 1);
    }

    /// <summary>
    /// Checks if <see cref="RetryRateLimitingStrategy"/> does not throw and checks how many times it repeated the calls.
    /// </summary>
    [Fact]
    public async void RetryRateLimitingStrategy()
    {
        // arrange
        const int expectedRetries = 5;
        var strategy = new RetryRateLimitingStrategy(expectedRetries, retryInterval: TimeSpan.FromMilliseconds(1));
        var showEndpoint = new ShowEndpoint(new TVmazeHttpClient(this.FlurlClient, strategy));
        var beforeCount = this.HttpTest.CallLog.Count;

        // act
        Func<Task> action = async () => await showEndpoint.GetShowMainInformationAsync(1);

        // assert
        await action.Should().ThrowAsync<TooManyRequestsException>();
        this.HttpTest.CallLog.Count.Should().Be(beforeCount + expectedRetries + 1);
    }

    public void Dispose()
    {
        this.HttpTest.Dispose();
    }
}