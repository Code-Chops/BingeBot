using BingeBot.Contracts.TVmaze.V1;
using BingeBot.Domain.Persons;

namespace BingeBot.Application.Adapters.TVmaze.V1;

public sealed record TVmazePersonAdapter : Adapter<Person, TVmazePersonContract>
{
	public override TVmazePersonContract ConvertToContract(Person person) 
		=> new()
	{
		Id = person.Id,
		Name = person.Name,
	};

	public override Person ConvertToObject(TVmazePersonContract contract)
	{
		return new Person(tVmazeId: new(contract.Id), name: new(contract.Name));
	}
}