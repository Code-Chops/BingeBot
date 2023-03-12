using BingeBot.Application.Adapters.TVmaze.V1;
using BingeBot.Infrastructure.Api.TVmaze;
using BingeBot.Infrastructure.Api.TVmaze.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BingeBot.Application.BackgroundTasks;

/// <summary>
/// A background service which synchronizes the TVmaze API shows and saves them to the database. It takes the following steps:
/// <list type="number">
/// <item>It checks the shows in the database and takes the latest TVmazeShowId and divides it by the maximum page size of TVmaze in order to retrieve the latest page.</item>
/// <item>It makes a requests to the TVmaze API to retrieve the shows of the current page.</item>
/// <item>If the list is empty it has finished the job and it quits.</item>
/// <item>It filters out the shows that are premiered on or after <seealso cref="MinimumShowPremieredDate"/>.</item>
/// <item>It stores the shows in a persistence layer (using a transaction scope, so every show is always completely inserted, or none).</item>
/// <item>It increments the current page and repeats from step 2.</item>
/// </list> 
/// <remarks>
/// Step 3 is not necessarily correct; there is a *very* small chance that TVmaze has deleted all the shows in a page and therefore returns an empty list.
/// I didn't have time anymore to fix this.
/// </remarks>
/// </summary>
public sealed class SynchronizationService : BackgroundService
{
	/// <summary>
	/// The minimum show premiered date (filtering). If a show doesn't have a premiered date, it will be ignored as well.
	/// </summary>
	private static readonly DateOnly MinimumShowPremieredDate = new(2014, 01, 01);

	/// <summary>
	/// Interval of each request to TVmaze.
	/// </summary>
	private static readonly TimeSpan Interval = TimeSpan.FromSeconds(1); 

	private IShowApplicationService ShowApplicationService { get; }
	private ITVmazeClient TVmazeClient { get; }
	private TVmazeShowAdapter ShowAdapterClient { get; } = new();
	private ILogger<SynchronizationService> Logger { get; }
	private PeriodicTimer Timer { get; } = new(Interval);

	public SynchronizationService(IShowApplicationService showApplicationService, ITVmazeClient tvMazeClient, ILogger<SynchronizationService> logger)
	{
		this.ShowApplicationService = showApplicationService ?? throw new ArgumentNullException(nameof(showApplicationService));
		this.TVmazeClient = tvMazeClient ?? throw new ArgumentNullException(nameof(tvMazeClient));
		this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		=> await this.Synchronize(cancellationToken);

	// Wrapped because it makes it easier to unit test.
	internal async Task Synchronize(CancellationToken cancellationToken)
	{
		this.Logger.Log(LogLevel.Information, $"{nameof(SynchronizationService)} started. Ignoring shows with a premiered date before {MinimumShowPremieredDate.ToShortDateString()}.");
		
		var id = await this.ShowApplicationService.GetHighestTVmazeShowId();
		var currentPage = id / Paging.MaximumPageSize;

		if (id == 0)
		{
			currentPage = 0;
			this.Logger.Log(LogLevel.Information, "No stored shows found. Starting at page {page}.", currentPage);
		}
		else
		{
			// Increment because we used a transaction scope: the previous page is fully inserted before.
			currentPage++;
			this.Logger.Log(LogLevel.Information, "The latest stored show has {id} (page {oldPage}). Resuming from page {page}.", id, currentPage - 1, currentPage);
		}
		
		while (await this.Timer.WaitForNextTickAsync(cancellationToken) && !cancellationToken.IsCancellationRequested)
		{
			this.Logger.Log(LogLevel.Information, "Retrieving shows from page {page}.", currentPage);
			
			var clientShows = (await this.TVmazeClient.ShowEndpoint.GetShowsAsync(currentPage)).ToList();

			// Finished with syncing.
			if (clientShows.Count == 0)
				break;
			
			var shows = clientShows
				.Where(show => show.Premiered is not null)
				.Select(this.ShowAdapterClient.ConvertToObject)
				.Where(show => show.PremieredDate >= MinimumShowPremieredDate);
			
			await this.ShowApplicationService.CreateShowsAsync(shows, cancellationToken);
			
			this.Logger.Log(LogLevel.Information, "Stored {showCount} shows.", clientShows.Count);

			currentPage++;
		}
		
		this.Logger.Log(LogLevel.Information, $"{nameof(SynchronizationService)} finished!");
	}
}