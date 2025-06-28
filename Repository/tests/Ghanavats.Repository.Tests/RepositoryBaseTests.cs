using Ghanavats.Repository.Tests.Configs;
using Ghanavats.Repository.Tests.DummyData;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Ghanavats.Repository.Tests;

public class RepositoryBaseTests : DummyDbSetup
{
    [Fact]
    internal void ShouldCorrectlyAssignDbContextInstance_WhenAnInstanceProvided()
    {
        //arrange/act/assert
        var action = new Action(() =>
        {
            var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);

            dummyRepository.ShouldNotBeNull();
            dummyRepository.GetType().BaseType.ShouldNotBeNull();
            dummyRepository.ShouldBeOfType<DummyRepository<TestEntity>>();
        });

        action.ShouldNotThrow();
    }

    [Fact]
    internal void ShouldThrowException_WhenDbContextInstanceNotProvidedToConstructor()
    {
        //arrange/act/assert
        var action = new Action(() => { _ = new DummyRepository<TestEntity>(null!); });

        action.ShouldThrow<ArgumentNullException>().Message.ShouldBe("Argument cannot be null or empty. (Parameter 'dbContext')");
    }

    [Fact]
    internal async Task ShouldCorrectlyAddNewItem()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        var entity = new TestEntity
        {
            TestProperty = "newValue"
        };
        
        //act
        var actual = await dummyRepository.AddAsync(entity);
        
        //assert
        actual.ShouldNotBeNull();
        actual.TestProperty.ShouldBeSameAs(entity.TestProperty);
        actual.Id.ShouldNotBe(0);
        actual.ShouldBeOfType<TestEntity>();
        
        DbContextForTest.TestEntities.Entry(entity).Entity.ShouldBeOfType<TestEntity>();
        DbContextForTest.TestEntities.Entry(entity).Entity.ShouldBeSameAs(entity);
    }
    
    [Fact]
    internal async Task ShouldCorrectlyUpdateAnItem()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        var entity = new TestEntity
        {
            Id = 2,
            TestProperty = "TestValue_Winter"
        };
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataFor());
        
        //act
        var actual = await dummyRepository.UpdateAsync(entity);
        
        //assert
        actual.ShouldNotBeNull();
        actual.TestProperty.ShouldBeSameAs(entity.TestProperty);
        actual.ShouldBeOfType<TestEntity>();

        var allRecordsOfTheEntity = await DbContextForTest.TestEntities.ToListAsync();
        allRecordsOfTheEntity
            .Where(x => x.Id == entity.Id)
            .Select(x => x.TestProperty).FirstOrDefault()
            .ShouldBeSameAs(actual.TestProperty);
    }
}
