namespace BingeBot.Domain.Shows;

/// <summary>
/// The show ID of the TVmaze api.
/// </summary>
[GenerateValueObject<int>(forbidParameterlessConstruction: false)]
public readonly partial record struct ShowTVmazeId;
