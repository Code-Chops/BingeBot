using Microsoft.Extensions.DependencyInjection;

namespace BingeBot.Domain;

public static class DomainRegistrationExtensions
{
	public static IServiceCollection AddDomainLayer(this IServiceCollection services)
	{
		// Nothing to do.
		
		return services;
	}
}