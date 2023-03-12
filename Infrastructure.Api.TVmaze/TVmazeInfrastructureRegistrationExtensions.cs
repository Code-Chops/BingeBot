using BingeBot.Contracts;
using BingeBot.Infrastructure.Api.TVmaze.Configuration;
using BingeBot.Infrastructure.Api.TVmaze.Http;
using BingeBot.Infrastructure.Api.TVmaze.V1;
using Flurl.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BingeBot.Infrastructure.Api.TVmaze;

public static class TVmazeInfrastructureRegistrationExtensions
{
	public static IServiceCollection AddTVmazeInfrastructureLayer(this IServiceCollection services, Settings settings)
	{
		services.AddSingleton<IShowEndpoint, ShowEndpoint>();
		services.AddSingleton<IPersonEndpoint, PersonEndpoint>();
		services.AddSingleton<HttpClient>();
		services.AddSingleton<IFlurlClient, FlurlClient>(_ =>
		{
			var flurlClient = new FlurlClient(new HttpClient());
			flurlClient.BaseUrl = settings.ConnectionStrings.TVmazeUrl;
			flurlClient.AllowAnyHttpStatus();
			return flurlClient;
		});

		services.AddSingleton<IRateLimitingStrategy, RetryRateLimitingStrategy>();
		services.AddSingleton<ITVmazeHttpClient, TVmazeHttpClient>();
		services.AddSingleton<ITVmazeClient, TVmazeClient>();
		
		return services;
	}
}