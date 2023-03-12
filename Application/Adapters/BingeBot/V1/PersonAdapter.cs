using BingeBot.Contracts.BingeBot.V1.Persons;
using BingeBot.Domain.Persons;

namespace BingeBot.Application.Adapters.BingeBot.V1;

public sealed record PersonAdapter : Adapter<Person, PersonContract>
{
	public override GetPersonResponse ConvertToContract(Person person) 
		=> new()
	{
		Id = person.Id,
		TVmazeId = person.TVmazeId,
		Name = person.Name,
	};

	public override Person ConvertToObject(PersonContract contract)
	{
		return new Person(
			tVmazeId: new(contract.TVmazeId), 
			name: new(contract.Name));
	}
}