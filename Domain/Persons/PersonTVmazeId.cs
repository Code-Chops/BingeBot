namespace BingeBot.Domain.Persons;

/// <summary>
/// The person ID of the TVmaze API.
/// </summary>
[GenerateValueObject<int>(forbidParameterlessConstruction: false)]
public readonly partial record struct PersonTVmazeId;