using Architect.EntityFramework.DbContextManagement;
using BingeBot.Application.Adapters.BingeBot.V1;
using BingeBot.Contracts.BingeBot.V1.Persons;
using BingeBot.Infrastructure.Databases;
using BingeBot.Infrastructure.Databases.Persons;
using Microsoft.Extensions.Logging;

namespace BingeBot.Application;

/// <inheritdoc />
public class PersonApplicationService : IPersonApplicationService
{
	private IDbContextProvider<DatabaseContext> DbContextProvider { get; }
	private IPersonRepo Repo { get; }
	private ILogger<PersonApplicationService> Logger { get; }
	private PersonFilterAdapter PersonFilterAdapter { get; } = new();
	private PersonAdapter PersonAdapter { get; } = new();
	
	public PersonApplicationService(IDbContextProvider<DatabaseContext> dbContextProvider, IPersonRepo repo, ILogger<PersonApplicationService> logger)
	{
		this.DbContextProvider = dbContextProvider ?? throw new ArgumentNullException(nameof(dbContextProvider));
		this.Repo = repo ?? throw new ArgumentNullException(nameof(repo));
		this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	public async Task CreatePersonAsync(CreatePersonRequest request, CancellationToken cancellationToken)
	{
		if (request is null) throw new ArgumentNullException(nameof(request));

		var person = this.PersonAdapter.ConvertToObject(request);

		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct) 
			=> await this.Repo.AddPersonAsync(person, ct));
	}
	
	public async Task<IEnumerable<GetPersonResponse>> GetFilteredPersonsAsync(GetPersonsRequest request, CancellationToken cancellationToken)
	{
		if (request is null) throw new ArgumentNullException(nameof(request));
		
		var filter = this.PersonFilterAdapter.ConvertToObject(request);
		
		var persons = await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct) 
			=> await this.Repo.GetPersonsByFilterAsync(filter, ct));

		return persons.Select(person => this.PersonAdapter.ConvertToContract(person));
	}

	public async Task CreateOrUpdatePersonAsync(int? id, UpdatePersonRequest request, CancellationToken cancellationToken)
	{
		if (request is null) throw new ArgumentNullException(nameof(request));

		var person = this.PersonAdapter.ConvertToObject(request);

		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct) 
			=> await this.Repo.UpdatePersonAsync(person, ct));
	}

	public async Task DeletePersonAsync(int personId, CancellationToken cancellationToken)
	{
		await this.DbContextProvider.ExecuteInDbContextScopeAsync(cancellationToken, async (_, ct)
			=> await this.Repo.DeletePersonAsync(new(personId), ct));
	}
}