using Architect.EntityFramework.DbContextManagement;
using BingeBot.Application.Adapters.BingeBot.V1;
using BingeBot.Contracts.BingeBot.V1.Shows;
using BingeBot.Domain.Shows;
using BingeBot.Infrastructure.Databases;
using BingeBot.Infrastructure.Databases.Shows;
using Microsoft.Extensions.Logging;

namespace BingeBot.Application;

/// <inheritdoc />
public class ShowApplicationService : IShowApplicationService
{
	private IDbContextProvider<DatabaseContext> DbContextProvider { get; }
	private IShowRepo Repo { get; }
	private ILogger<ShowApplicationService> Logger { get; }
	private ShowFilterAdapter ShowFilterAdapter { get; } = new();
	private ShowAdapterApi ShowAdapterApi { get; } = new();

	public ShowApplicationService(IDbContextProvider<DatabaseContext> dbContextProvider, IShowRepo repo, ILogger<ShowApplicationService> logger)
	{
		this.DbContextProvider = dbContextProvider ?? throw new ArgumentNullException(nameof(dbContextProvider));
		this.Repo = repo ?? throw new ArgumentNullException(nameof(repo));
		this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task CreateShowAsync(CreateShowRequest showContract, CancellationToken cancellationToken)
	{
		if (showContract is null) throw new ArgumentNullException(nameof(showContract));

		var show = this.ShowAdapterApi.ConvertToObject(showContract);

		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct) 
			=> await this.Repo.AddShowAsync(show, ct));
	}
	
	async Task IShowApplicationService.CreateShowsAsync(IEnumerable<Show> shows, CancellationToken cancellationToken)
	{
		if (shows is null) throw new ArgumentNullException(nameof(shows));
		
		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct) 
			=> await this.Repo.AddShowsAsync(shows, ct));
	}
	
	public async Task<IEnumerable<GetShowResponse>> GetFilteredShowsAsync(GetShowsRequest request, CancellationToken cancellationToken)
	{
		if (request is null) throw new ArgumentNullException(nameof(request));
		
		var filter = this.ShowFilterAdapter.ConvertToObject(request);

		var shows = await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct)  
			=> await this.Repo.GetShowsByFilterAsync(filter, ct));
		
		return shows.Select(show => this.ShowAdapterApi.ConvertToContract(show));
	}

	public async Task CreateOrUpdateShowAsync(int? id, UpdateShowRequest request, CancellationToken cancellationToken)
	{
		if (request is null) throw new ArgumentNullException(nameof(request));

		var show = this.ShowAdapterApi.ConvertToObject(request);

		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct) 
			=> await this.Repo.CreateOrUpdateShowAsync(show, ct));
	}

	public async Task DeleteShowAsync(int showId, CancellationToken cancellationToken)
	{
		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct)
			=> await this.Repo.DeleteShowAsync(new(showId), ct));
	}

	/// <inheritdoc />
	async Task<ShowTVmazeId> IShowApplicationService.GetHighestTVmazeShowId()
	{
		return await this.DbContextProvider.ExecuteInDbContextScopeAsync(async _ 
			=> await this.Repo.GetHighestTVmazeShowId());
	}
}