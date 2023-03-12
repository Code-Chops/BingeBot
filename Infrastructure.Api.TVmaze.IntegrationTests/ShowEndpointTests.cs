namespace BingeBot.Infrastructure.Api.TVmaze.IntegrationTests;

public class ShowEndpointTests
{
    private IShowEndpoint ShowEndpoint { get; }

    public ShowEndpointTests(ITVmazeHttpClient tVmazeHttpClient)
    {
        this.ShowEndpoint = new ShowEndpoint(tVmazeHttpClient);
    }

    /// <summary>
    /// Tests if the retrieval of show main information of show #1 is not null.
    /// </summary>
    [Fact]
    public async void GetShowMainInformationAsync_ValidParameter_Success()
    {
        // arrange
        const int validShowId = 1;

        // act
        var response = await this.ShowEndpoint.GetShowMainInformationAsync(validShowId);

        // assert
        response.Should().NotBeNull();
    }

    /// <summary>
    /// Tests if retrieving the first page of shows is not null or empty.
    /// </summary>
    [Fact]
    public async void GetShowsAsync_ValidParameters_Success()
    {
        // arrange 
        const int validPage = 1;

        // act
        var response = await this.ShowEndpoint.GetShowsAsync(validPage);

        // assert
        response.Should().NotBeNull().And.NotBeEmpty();
    }

    /// <summary>
    /// Tests if retrieving a page that is out of range returns and empty result and not null.
    /// </summary>
    [Fact]
    public async void GetShowsAsync_ValidParameters_NotFound()
    {
        // arrange 
        const int notFoundPage = int.MaxValue;

        // act
        var response = await this.ShowEndpoint.GetShowsAsync(notFoundPage);

        // assert
        response.Should().NotBeNull().And.BeEmpty();
    }
}