using Ghanavats.Repository.Tests.DummyData;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace Ghanavats.Repository.Tests.Configs;

public class DummyDbSetup : IAsyncLifetime
{
    protected DummyDbContext DbContextForTest { get; private set; } = null!;
    private DbContextOptions<DummyDbContext> DbContextOptionsForTest { get; set; } = null!;
    private string ConnectionString { get; set; } = string.Empty;
    private MsSqlContainer _sqlContainer = null!;
    
    public async Task InitializeAsync()
    {
        _sqlContainer = TestContainer();
        await _sqlContainer.StartAsync();

        ConnectionString = _sqlContainer.GetConnectionString();
        
        DbContextOptionsForTest = new DbContextOptionsBuilder<DummyDbContext>()
            .UseSqlServer(ConnectionString)
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .Options;

        DbContextForTest = new DummyDbContext(DbContextOptionsForTest);

        await using var dbContextForTestLocal = new DummyDbContext(DbContextOptionsForTest);
        await dbContextForTestLocal.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContextForTest.DisposeAsync();
        await _sqlContainer.StopAsync();
        await _sqlContainer.DisposeAsync();
    }

    /// <summary>
    /// A comment to my future self; You need to define the constraint for type TEntity.
    /// Otherwise, your DB Context won't be able to find the type you are trying to bulk insert for.
    /// </summary>
    /// <param name="entities"></param>
    /// <typeparam name="TEntity"></typeparam>
    private protected async Task SeedDataAsync<TEntity>(IReadOnlyCollection<TEntity> entities) where TEntity : class
    {
        await using var dbContextForTestLocal = new DummyDbContext(DbContextOptionsForTest);
        
        dbContextForTestLocal.AddRange(entities);
        await dbContextForTestLocal.SaveChangesAsync();
    }

    private static MsSqlContainer TestContainer()
    {
        return new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithName("TestDb")
            .WithPassword("P@ssw0rd_123")
            .WithPortBinding(1433, true)
            .WithReuse(false)
            .Build();
    }
}
