namespace BingeBot.Contracts.BingeBot.V1.Shows;

/// <summary>
/// Show data which is exposed to external sources (third parties).
/// </summary>
public abstract record ShowContract : Contract
{
	public required int TVmazeId { get; init; }
	public required string Name { get; init; }
	public required List<string> Genres { get; init; }
	public required string? Language { get; init; }
	public required string Premiered { get; init; }
	public required string? Summary { get; init; }
}
