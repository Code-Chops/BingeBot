using BingeBot.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace BingeBot.Infrastructure.Api.TVmaze.IntegrationTests;

public class Startup
{
	public void ConfigureServices(IServiceCollection services)
	{
		var settings = new Settings()
		{
			ConnectionStrings = new ConnectionStrings()
			{
				TVmazeUrl = "https://api.tvmaze.com/",
				Database = "",
			},
		};
		
		services.AddTVmazeInfrastructureLayer(settings);
	}
	
	public void Configure(ILoggerFactory loggerFactory, ITestOutputHelperAccessor accessor) =>
		loggerFactory.AddProvider(new XunitTestOutputLoggerProvider(accessor));
}