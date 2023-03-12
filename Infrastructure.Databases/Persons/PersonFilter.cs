using BingeBot.Domain.Persons;

namespace BingeBot.Infrastructure.Databases.Persons;

/// <summary>
/// Filtering of <see cref="BingeBot.Domain.Persons.Person"/> queries.  
/// </summary>
public sealed record PersonFilter : PagingFilter<Person>, IFilterWithPaging<Person>
{
	public PersonId? Id { get; init; }
	public PersonTVmazeId? TVmazeId { get; init; }
	public PersonName? Name { get; init; }
	
	public PersonFilter(int page, int? size)
		: base(page, size)
	{
	}
	
	public IQueryable<Person> ApplyFilter(IQueryable<Person> persons)
	{
		if (this.Id is not null)
			persons = persons.Where(show => show.Id == this.Id);

		if (this.TVmazeId is not null)
			persons = persons.Where(show => show.TVmazeId == this.TVmazeId);

		if (this.Name is not null)
			persons = persons.Where(show => show.Name == this.Name);
		
		return persons;
	}
}