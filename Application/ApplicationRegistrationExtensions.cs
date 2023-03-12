using System.Globalization;
using BingeBot.Contracts;
using BingeBot.Domain;
using BingeBot.Infrastructure.Api.TVmaze;
using BingeBot.Infrastructure.Databases;
using Microsoft.Extensions.DependencyInjection;

namespace BingeBot.Application;

public static class ApplicationRegistrationExtensions
{
	public static IServiceCollection AddApplicationLayer(this IServiceCollection services, Settings settings)
	{
		// Use the invariant culture throughout the application.
		CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

		services.AddDomainLayer();
		services.AddDatabaseInfrastructureLayer(settings);
		services.AddTVmazeInfrastructureLayer(settings);
		
		return services;
	}
}