namespace BingeBot.Contracts.TVmaze.V1;

public sealed record TVmazeShowContract : Contract
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string? Language { get; init; }
    public required string? Premiered { get; init; }
    public required List<string> Genres { get; init; }
    public required string? Summary { get; init; }
}