using System.Collections.Immutable;
using BingeBot.Contracts.TVmaze.V1;
using BingeBot.Domain.ShowGenres;
using BingeBot.Domain.Shows;

namespace BingeBot.Application.Adapters.TVmaze.V1;

public sealed record TVmazeShowAdapter : Adapter<Show, TVmazeShowContract>
{
	public override TVmazeShowContract ConvertToContract(Show show)
	{
		return new TVmazeShowContract()
		{
			Id = show.Id,
			Language = show.Language,
			Name = show.Name,
			Premiered = show.PremieredDate.ToString(),
			Genres = show.Genres.Select(genre => (string)genre.Name).ToList(),
			Summary = show.Summary,
		};
	}

	public override Show ConvertToObject(TVmazeShowContract contract)
	{
		return new Show(
			tVmazeId: new(contract.Id), 
			name: new(contract.Name), 
			genres: new List<ShowGenre>(contract.Genres.Select(genre => new ShowGenre(new(genre))).ToImmutableList()),
			language: new(contract.Language), 
			premieredDate: new(contract.Premiered ?? throw new ArgumentNullException(nameof(contract.Premiered))), 
			summary: new(contract.Summary));
	}
}