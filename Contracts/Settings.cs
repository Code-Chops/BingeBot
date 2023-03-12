using System.ComponentModel.DataAnnotations;

namespace BingeBot.Contracts;

/// <summary>
/// Settings which are retrieved from appsettings.json.
/// </summary>
public record Settings
{
	public const string SectionName = "Settings"; // Don't use nameof() because refactoring could change the name and that would lead to unexpected behaviour.
	
	// I don't know why the Required attributes don't work :(
	[Required] 
	public required ConnectionStrings ConnectionStrings { get; init; }
}

public record ConnectionStrings
{
	[Required] 
	public required string Database { get; init; }
	
	[Required] 
	public required string TVmazeUrl { get; init; }
}