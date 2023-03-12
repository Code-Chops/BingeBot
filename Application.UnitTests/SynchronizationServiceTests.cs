using BingeBot.Application.BackgroundTasks;
using BingeBot.Contracts.TVmaze.V1;
using BingeBot.Domain.Shows;
using BingeBot.Infrastructure.Api.TVmaze;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace BingeBot.Application.UnitTests;

public class SynchronizationServiceTests
{
	[Fact]
	public async Task ShouldBegin_WithPageZero()
	{
		// Arrange
		var showServiceMock = new Mock<IShowApplicationService>();
		var showEndpointMock = new Mock<IShowEndpoint>();
		var personEndpointMock = new Mock<IPersonEndpoint>();
		
		var tVmazeClient = new TVmazeClient(showEndpointMock.Object, personEndpoint: personEndpointMock.Object);
		var service = new SynchronizationService(showServiceMock.Object, tVmazeClient, new NullLogger<SynchronizationService>());
		var ctSource = new CancellationTokenSource();
		
		showServiceMock.Setup(s => s.GetHighestTVmazeShowId()).ReturnsAsync(() => new ShowTVmazeId(0));
		
		// Act
		await service.Synchronize(ctSource.Token);
		
		// Assert
		showEndpointMock.Verify(s => s.GetShowsAsync(It.Is<int>(p => p == 0)));
	}
	
	[Fact]
	public async Task ShouldStop_WhenEmptyResult_FromTVmaze()
	{
		// Arrange
		var showServiceMock = new Mock<IShowApplicationService>();
		var showEndpointMock = new Mock<IShowEndpoint>();
		var personEndpointMock = new Mock<IPersonEndpoint>();
		
		var tVmazeClient = new TVmazeClient(showEndpointMock.Object, personEndpointMock.Object);
		var service = new SynchronizationService(showServiceMock.Object, tVmazeClient, new NullLogger<SynchronizationService>());
		var ctSource = new CancellationTokenSource();
		
		showServiceMock.Setup(s => s.GetHighestTVmazeShowId()).ReturnsAsync(() => new ShowTVmazeId(0));
		showEndpointMock.Setup(s => s.GetShowsAsync(It.IsAny<int>())).ReturnsAsync(new List<TVmazeShowContract>());

		// Act
		await service.Synchronize(ctSource.Token);
		
		// Assert
		showEndpointMock.Verify(s => s.GetShowsAsync(It.IsAny<int>()), Times.Once);
	}
}