namespace BingeBot.Contracts.BingeBot.V1.Persons;

/// <summary>
/// Person data which is exposed to external sources (third parties).
/// </summary>
public abstract record PersonContract : Contract
{
	public int TVmazeId { get; init; }
	public required string Name { get; init; }
}
