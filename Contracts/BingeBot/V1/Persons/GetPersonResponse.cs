namespace BingeBot.Contracts.BingeBot.V1.Persons;

public sealed record GetPersonResponse : PersonContract
{
	public int Id { get; init; }
}