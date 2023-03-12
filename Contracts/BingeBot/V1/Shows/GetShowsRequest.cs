namespace BingeBot.Contracts.BingeBot.V1.Shows;

public sealed record GetShowsRequest : BingeBotPagingRequest
{
	public int? Id { get; init; }
	public int? TVmazeId { get; init; }
	public string? Name { get; init; }
	public DateOnly? PremieredDate { get; init; }
}