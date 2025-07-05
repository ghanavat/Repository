using Microsoft.EntityFrameworkCore;

namespace Ghanavats.Repository.Tests.DummyData;

/// <summary>
/// A sneaky fake Db Context class.
/// It is only used to help with testing Repository Base as it needs an instance of DbContext.
/// </summary>
public class DummyDbContext : DbContext
{
    public DbSet<TestEntity> TestEntities => Set<TestEntity>();
    public DbSet<TestEntityChild> TestEntityChildren => Set<TestEntityChild>();

    public DummyDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasMany(x => x.TestEntityChildren)
                .WithOne(x => x.TestEntity)
                .HasForeignKey(x => x.TestEntityId)
                .IsRequired();
        });

        modelBuilder.Entity<TestEntityChild>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.TestEntity);
        });
    }
}

public class TestEntity
{
    public int Id { get; init; }
    public string TestProperty { get; init; } = string.Empty;
    public IEnumerable<TestEntityChild> TestEntityChildren { get; init; } = new List<TestEntityChild>();
}

public class TestEntityChild
{
    public int Id { get; init; }
    public int TestEntityId { get; init; }
    public string ChildName { get; init; } = string.Empty;
    public TestEntity TestEntity { get; init; } = null!;
}
