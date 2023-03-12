﻿namespace BingeBot.Contracts.TVmaze.V1;

public sealed record TVmazePersonContract : Contract
{
    public required int Id { get; init; }
    public required string Name { get; init; }
}