namespace BingeBot.Contracts.BingeBot.V1.Persons;

public sealed record GetPersonsRequest : BingeBotPagingRequest
{
	public int? Id { get; init; }
	public int? TVmazeId { get; init; }
	public string? Name { get; init; }
}