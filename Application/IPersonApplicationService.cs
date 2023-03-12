using BingeBot.Contracts.BingeBot.V1.Persons;

namespace BingeBot.Application;

/// <summary>
/// Contains CRUD methods and other actions on a person or a list of persons.
/// </summary>
public interface IPersonApplicationService
{
	Task CreatePersonAsync(CreatePersonRequest request, CancellationToken cancellationToken);
	Task<IEnumerable<GetPersonResponse>> GetFilteredPersonsAsync(GetPersonsRequest request, CancellationToken cancellationToken);
	Task CreateOrUpdatePersonAsync(int? id, UpdatePersonRequest request, CancellationToken cancellationToken);
	Task DeletePersonAsync(int personId, CancellationToken cancellationToken);
}