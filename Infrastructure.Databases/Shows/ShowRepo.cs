using System.Data;
using BingeBot.Domain.Shows;

namespace BingeBot.Infrastructure.Databases.Shows;

public sealed class ShowRepo : IShowRepo
{
	private DatabaseContext DbContext => this.DbContextAccessor.CurrentDbContext;

	private IDbContextAccessor<DatabaseContext> DbContextAccessor { get; }
   
	public ShowRepo(IDbContextAccessor<DatabaseContext> dbContextAccessor)
	{
		this.DbContextAccessor = dbContextAccessor;
	}

	public async Task AddShowAsync(Show show, CancellationToken cancellationToken)
	{
		await this.DbContext.Shows.AddAsync(show, cancellationToken);
		await this.DbContext.SaveChangesAsync(cancellationToken);
	}

	/// <summary>
	/// Add shows to the database in a transaction scope.
	/// </summary>
	public async Task AddShowsAsync(IEnumerable<Show> shows, CancellationToken cancellationToken)
	{
		await using var dbContextTransaction = await this.DbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
		await this.DbContext.Shows.AddRangeAsync(shows, cancellationToken);
		await this.DbContext.SaveChangesAsync(cancellationToken);
		await dbContextTransaction.CommitAsync(cancellationToken);
	}
	
	public Task<List<Show>> GetShowsByFilterAsync(ShowFilter filter, CancellationToken cancellationToken)
	{
		return this.DbContext
			.Shows
			.FilterWithPaging<Show, ShowId>(filter)
			.OrderByDescending(show => show.PremieredDate)
			.ToListAsync(cancellationToken);
	}

	public Task CreateOrUpdateShowAsync(Show show, CancellationToken cancellationToken)
	{
		this.DbContext.Entry(show).State = show.Id == 0 ? 
			EntityState.Added : 
			EntityState.Modified; 
		
		return this.DbContext.SaveChangesAsync(cancellationToken);
	}

	// Luckily execute delete is now FINALLY supported in EF 7. So we don't have to create an (invalid state) empty entity anymore that only contains an ID.
	// The old approach was also more resource consuming.
	// https://timdeschryver.dev/blog/new-in-entity-framework-7-bulk-operations-with-executedelete-and-executeupdate#setting-the-stage
	public Task DeleteShowAsync(ShowId id, CancellationToken cancellationToken)
	{
		return this.DbContext
			.Shows
			.Where(show => show.Id == id)
			.ExecuteDeleteAsync(cancellationToken: cancellationToken);
	}

	/// <inheritdoc /> 
	public async Task<ShowTVmazeId> GetHighestTVmazeShowId()
	{
		return (await this.DbContext
			.Shows
			.Filter(HighestTVmazeIdFilter)
			.OrderByDescending(show => show.Id)
			.FirstOrDefaultAsync())
			?.TVmazeId ?? new ShowTVmazeId(0);
	}
	
	private static readonly HighestTVmazeIdFilter HighestTVmazeIdFilter = new();
}