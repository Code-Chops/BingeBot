namespace BingeBot.Infrastructure.Api.TVmaze.IntegrationTests;

public class PersonEndpointTests
{
    public IPersonEndpoint PersonEndpoint { get; }

    public PersonEndpointTests(ITVmazeHttpClient tVmazeHttpClient)
    {
        this.PersonEndpoint = new PersonEndpoint(tVmazeHttpClient);
    }

    /// <summary>
    /// Tests if the retrieval of person main information of person #1 is not null.
    /// </summary>
    [Fact]
    public async void GetPersonMainInformationAsync_NotNull()
    {
        // arrange
        const int validPersonId = 1;

        // act
        var response = await this.PersonEndpoint.GetPersonMainInformationAsync(validPersonId);

        // assert
        response.Should().NotBeNull();
    }

    /// <summary>
    /// Tests if retrieving the first page of persons is not null or empty.
    /// </summary>
    [Fact]
    public async void GetPersonsAsync_ValidResults()
    {
        // arrange 
        const int validPage = 1;

        // act
        var response = await this.PersonEndpoint.GetPersonsAsync(validPage);

        // assert
        response.Should().NotBeNull().And.NotBeEmpty();
    }

    /// <summary>
    /// Tests if retrieving a page that is out of range returns and empty result and not null.
    /// </summary>
    [Fact]
    public async void GetPersonsAsync_MaximumPage_NoResults()
    {
        // arrange 
        const int notFoundPage = int.MaxValue;

        // act
        var response = await this.PersonEndpoint.GetPersonsAsync(notFoundPage);

        // assert
        response.Should().NotBeNull().And.BeEmpty();
    }
}