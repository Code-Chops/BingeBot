using BingeBot.Contracts.BingeBot.V1.Persons;
using BingeBot.Infrastructure.Databases.Persons;

namespace BingeBot.Application.Adapters.BingeBot.V1;

public sealed record PersonFilterAdapter : Adapter<PersonFilter, GetPersonsRequest>
{
	public override GetPersonsRequest ConvertToContract(PersonFilter filter)
	{
		return new GetPersonsRequest()
		{
			Id = filter.Id,
			TVmazeId = filter.TVmazeId,
			
			Page = filter.Page,
			Size = filter.Size ?? 0,
		};
	}

	public override PersonFilter ConvertToObject(GetPersonsRequest contract)
	{
		return new PersonFilter(page: contract.Page, size: contract.Size)
		{
			Id = contract.Id is { } id ? new(id) : null, 
			TVmazeId = contract.TVmazeId is { } tvMazeId ? new(tvMazeId) : null, 
			Name = contract.Name is { } name ? new(name) : null,
		};
	}
}