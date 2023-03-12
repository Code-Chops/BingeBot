using BingeBot.Contracts.BingeBot.V1.Shows;
using BingeBot.Infrastructure.Databases.Shows;

namespace BingeBot.Application.Adapters.BingeBot.V1;

public sealed record ShowFilterAdapter : Adapter<ShowFilter, GetShowsRequest>
{
	public override GetShowsRequest ConvertToContract(ShowFilter filter)
	{
		return new GetShowsRequest()
		{
			Id = filter.Id,
			TVmazeId = filter.TVmazeId,
			Name = filter.Name,
			PremieredDate = filter.PremieredDate,
			
			Page = filter.Page,
			Size = filter.Size ?? 0,
		};
	}

	public override ShowFilter ConvertToObject(GetShowsRequest contract)
	{
		return new ShowFilter(contract.Page, contract.Size)
		{
			Id = contract.Id is { } id ? new(id) : null,
			TVmazeId = contract.TVmazeId is { } tvMazeId ? new(tvMazeId) : null,
			Name = contract.Name is { } name ? new(name) : null,
			PremieredDate = contract.PremieredDate is { } premieredDate ? new(premieredDate) : null,
		};
	}
}