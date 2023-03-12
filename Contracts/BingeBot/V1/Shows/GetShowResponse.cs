namespace BingeBot.Contracts.BingeBot.V1.Shows;

public sealed record GetShowResponse : ShowContract
{
	public int Id { get; init; }
}