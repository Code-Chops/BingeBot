using BingeBot.Contracts;
using BingeBot.Infrastructure.Databases.Persons;
using BingeBot.Infrastructure.Databases.Shows;
using Microsoft.Extensions.DependencyInjection;

namespace BingeBot.Infrastructure.Databases;

public static class DatabaseInfrastructureRegistrationExtensions
{
	public static IServiceCollection AddDatabaseInfrastructureLayer(this IServiceCollection services, Settings settings)
	{
		// Register the DbContext with EF factory-based extensions.
		services.AddPooledDbContextFactory<DatabaseContext>(context => context.UseSql(settings).EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true));
		services.AddDbContextScope<DatabaseContext>();
        services.AddSingleton<IShowRepo, ShowRepo>();
        services.AddSingleton<IPersonRepo, PersonRepo>();
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        return services;
	}
	
	public static DatabaseContext CreateDbContext(string[] args)
	{
		var settings = new Settings()
		{
			ConnectionStrings = new()
			{
				Database = "Server=tcp:localhost;Database=BingeBot;User=sa;Password=BingeBot123!;TrustServerCertificate=True;",
				TVmazeUrl = ""
			}
		};
			
		var options = new DbContextOptionsBuilder<DatabaseContext>().UseSql(settings);

		return new DatabaseContext(options.Options);
	}
	
	public static DbContextOptionsBuilder UseSql(this DbContextOptionsBuilder context, Settings settings) 
		=> context.UseSqlServer(settings.ConnectionStrings.Database, o => o.EnableRetryOnFailure());
}