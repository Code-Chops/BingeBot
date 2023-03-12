using System.Collections.Immutable;
using BingeBot.Contracts.BingeBot.V1.Shows;
using BingeBot.Domain.ShowGenres;
using BingeBot.Domain.Shows;

namespace BingeBot.Application.Adapters.BingeBot.V1;

public sealed record ShowAdapterApi : Adapter<Show, ShowContract>
{
	public override GetShowResponse ConvertToContract(Show show) 
		=> new()
	{
		Id = show.Id,
		TVmazeId = show.TVmazeId ?? 0,
		Genres = show.Genres.Select(genre => (string)genre.Name).ToList(),
		Language = show.Language,
		Name = show.Name,
		Premiered = show.PremieredDate.ToString(),
		Summary = show.Summary,
	};

	public override Show ConvertToObject(ShowContract contract) 
		=> new (
			tVmazeId: new(contract.TVmazeId), 
			name: new(contract.Name), 
			genres: new List<ShowGenre>(contract.Genres.Select(genre => new ShowGenre(new(genre))).ToImmutableList()), 
			language: new(contract.Language), 
			premieredDate: new(contract.Premiered), 
			summary: new(contract.Summary));
}