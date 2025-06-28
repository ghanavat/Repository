using Microsoft.EntityFrameworkCore;

namespace Ghanavats.Repository.Tests.DummyData;

/// <summary>
/// A sneaky fake Db Context class.
/// It is only used to help with testing Repository Base as it needs an instance of DbContext.
/// </summary>
public class DummyDbContext : DbContext
{
    public DbSet<TestEntity> TestEntities => Set<TestEntity>();
    
    public DummyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }
}

public class TestEntity
{
    public int Id { get; init; }
    public string TestProperty { get; init; } = string.Empty;
}
