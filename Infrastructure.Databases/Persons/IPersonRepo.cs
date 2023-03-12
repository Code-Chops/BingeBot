using BingeBot.Domain.Persons;

namespace BingeBot.Infrastructure.Databases.Persons;

public interface IPersonRepo
{
	Task<List<Person>> GetPersonsByFilterAsync(PersonFilter filter, CancellationToken cancellationToken);
	Task AddPersonAsync(Person person, CancellationToken cancellationToken);
	Task UpdatePersonAsync(Person person, CancellationToken cancellationToken);
	Task DeletePersonAsync(PersonId id, CancellationToken cancellationToken);
}