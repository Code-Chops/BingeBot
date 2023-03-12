using BingeBot.Domain.Persons;
using BingeBot.Domain.ShowGenres;
using BingeBot.Domain.Shows;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BingeBot.Infrastructure.Databases;

public sealed class DatabaseContext : DbContext, IDesignTimeDbContextFactory<DatabaseContext>
{
	public DbSet<Show> Shows { get; set; } = default!;
	public DbSet<Person> Persons { get; set; } = default!;
	
	public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }

	// Needed for EF code first
	public DatabaseContext()
	{
	}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    modelBuilder.Entity<Show>().HasIndex(s => s.Id).IsUnique();
        modelBuilder.Entity<Show>().HasIndex(s => s.TVmazeId).IsUnique();
        
        modelBuilder.Entity<Show>().Property(s => s.Id).HasColumnName("id").HasConversion<CastingConverter<ShowId, int>>().ValueGeneratedOnAdd().UseIdentityColumn();
        modelBuilder.Entity<Show>().Property(s => s.TVmazeId).HasColumnName("tvmaze_id").HasConversion<CastingConverter<ShowTVmazeId, int>>().IsRequired(false);
        modelBuilder.Entity<Show>().Property(s => s.Name).HasColumnName("name").HasConversion<CastingConverter<ShowName, string>>().Metadata.SetValueComparer(ValueComparerFactory<ShowName>.Instance);
        modelBuilder.Entity<Show>().Property(s => s.Language).HasColumnName("language").HasConversion<CastingConverter<ShowLanguage, string>>().Metadata.SetValueComparer(ValueComparerFactory<ShowLanguage>.Instance);
        modelBuilder.Entity<Show>().Property(s => s.PremieredDate).HasColumnName("premiered_date").HasConversion(d => d.Value.ToShortDateString(), d => new(DateOnly.Parse(d, null), null));
        modelBuilder.Entity<Show>().Property(s => s.Summary).HasColumnName("summary").HasConversion<CastingConverter<ShowSummary, string>>().Metadata.SetValueComparer(ValueComparerFactory<ShowSummary>.Instance);
        modelBuilder.Entity<Show>().HasMany(s => s.Genres).WithOne(g => g.Show).HasForeignKey(g => g.ShowId).HasConstraintName("ForeignKey_Genre_Show");

        
        modelBuilder.Entity<ShowGenre>().HasIndex(g => g.Id).IsUnique();
        
        modelBuilder.Entity<ShowGenre>().Property(g => g.Id).HasColumnName("id").HasConversion<CastingConverter<ShowGenreId, int>>().ValueGeneratedOnAdd().UseIdentityColumn();
        modelBuilder.Entity<ShowGenre>().Property(g => g.ShowId).HasColumnName("show_id").HasConversion<CastingConverter<ShowId, int>>();
        modelBuilder.Entity<ShowGenre>().Property(g => g.Name).HasColumnName("name").HasConversion<CastingConverter<ShowGenreName, string>>().Metadata.SetValueComparer(ValueComparerFactory<ShowGenreName>.Instance);

        
        modelBuilder.Entity<Person>().HasIndex(p => p.Id).IsUnique();
        modelBuilder.Entity<Person>().HasIndex(p => p.TVmazeId).IsUnique();

        modelBuilder.Entity<Person>().Property(p => p.Id).HasColumnName("id").HasConversion<CastingConverter<PersonId, int>>().ValueGeneratedOnAdd().UseIdentityColumn();
        modelBuilder.Entity<Person>().Property(p => p.TVmazeId).HasColumnName("tvmaze_id").HasConversion<CastingConverter<PersonTVmazeId, int>>();
        modelBuilder.Entity<Person>().Property(p => p.Name).HasColumnName("name").HasConversion<CastingConverter<PersonName, string>>().Metadata.SetValueComparer(ValueComparerFactory<PersonName>.Instance);
    }

    public DatabaseContext CreateDbContext(string[] args)
		=> DatabaseInfrastructureRegistrationExtensions.CreateDbContext(args);
}

/// <summary>
/// I don't know why I have to do this, because my auto-generated value objects provide the casting it needs.
/// I guess it's because they also implement IEnumerable it prefers to take that route. Strange behaviour.
/// </summary>
public static class ValueComparerFactory<T>
	where T : IEnumerable<char>, ICreatable<T, string>, IDomainObject
{
	public static ValueComparer Instance { get; } = new ValueComparer<T>(
		(c1, c2) => c1!.SequenceEqual(c2!),
		c => c.Aggregate(0, HashCode.Combine),
		c => CreateSnapshot(c));

	public static T CreateSnapshot(T value) 
		=> T.Create(new String(value.ToArray()));
}