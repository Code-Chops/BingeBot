namespace BingeBot.Domain.Persons;

/// <summary>
/// Member of a cast of a <see cref="Shows.Show"/>.
/// </summary>
/// <remarks>The <see cref="PersonId"/> should not be communicated externally!</remarks>
[GenerateIdentity<int>(nameof(PersonId))]
public sealed class Person : Entity<PersonId>
{
	public PersonTVmazeId TVmazeId { get; }
	public PersonName Name { get; }

	/* Etc... */
	
	/// <summary>
	/// Used by adapters.
	/// </summary>
	internal Person(PersonId id, PersonTVmazeId tVmazeId, PersonName name)
	{
		this.Id = id;
		this.TVmazeId = tVmazeId;
		this.Name = name;
	}
	
	/// <summary>
	/// Used for creating a non-existing entity in the database. 
	/// </summary>
	public Person(PersonTVmazeId tVmazeId, PersonName name)
	{
		this.Id = default;	// Is being assigned by the database
		this.TVmazeId = tVmazeId;
		this.Name = name;
	}
	
	#region Entity Framework Constructor
	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.    
	[Obsolete("Reconstitution only.")]
	// ReSharper disable once UnusedMember.Local
	private Person()
	{
	}
	#pragma warning restore
	#endregion
}