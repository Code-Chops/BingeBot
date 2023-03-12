using BingeBot.Domain.Persons;

namespace BingeBot.Infrastructure.Databases.Persons;

public sealed class PersonRepo : IPersonRepo
{
	private DatabaseContext DbContext => this.DbContextAccessor.CurrentDbContext;

	private IDbContextAccessor<DatabaseContext> DbContextAccessor { get; }
   
	public PersonRepo(IDbContextAccessor<DatabaseContext> dbContextAccessor)
	{
		this.DbContextAccessor = dbContextAccessor;
	}

	public async Task AddPersonAsync(Person person, CancellationToken cancellationToken)
	{
		await this.DbContext.Persons.AddAsync(person, cancellationToken);
		await this.DbContext.SaveChangesAsync(cancellationToken);
	}

	public Task<List<Person>> GetPersonsByFilterAsync(PersonFilter filter, CancellationToken cancellationToken)
	{
		return this.DbContext
			.Persons
			.FilterWithPaging<Person, PersonId>(filter)
			.OrderBy(person => person.Name)
			.ToListAsync(cancellationToken);
	}

	public Task UpdatePersonAsync(Person person, CancellationToken cancellationToken)
	{
		this.DbContext.Entry(person).State = person.Id == 0 ? 
			EntityState.Added : 
			EntityState.Modified; 

		return this.DbContext.SaveChangesAsync(cancellationToken);
	}

	// Luckily execute delete is now FINALLY supported in EF 7. So we don't have to create an (invalid state) empty entity anymore that only contains an ID.
	// The old approach was also more resource consuming.
	// https://timdeschryver.dev/blog/new-in-entity-framework-7-bulk-operations-with-executedelete-and-executeupdate#setting-the-stage.
	public Task DeletePersonAsync(PersonId id, CancellationToken cancellationToken)
	{
		return this.DbContext
			.Persons
			.Where(person => person.Id == id)
			.ExecuteDeleteAsync(cancellationToken: cancellationToken);
	}
}