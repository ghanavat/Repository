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
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataForTestEntity());
        
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
    
    [Fact]
    internal async Task ShouldCorrectlyDeleteAnItem()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        var entity = new TestEntity
        {
            Id = 3
        };
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataForTestEntity());
        
        //act
        await dummyRepository.DeleteAsync(entity);
        
        //assert
        var allRecordsOfTheEntity = await DbContextForTest.TestEntities.ToListAsync();
        var deletedRecordResult = allRecordsOfTheEntity
            .Any(x => x.Id == entity.Id);
        deletedRecordResult.ShouldBeFalse();
    }
    
    [Fact]
    internal async Task ShouldCorrectlyRetrieveAnItemById()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        var expectedId = 5;
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataForTestEntity());
        
        //act
        var actual = await dummyRepository.GetByIdAsync(expectedId);
        
        //assert
        actual.ShouldNotBeNull();
        actual.Id.ShouldBe(expectedId);
    }
    
    [Fact]
    internal async Task ShouldCorrectlyRetrieveAnItemByIdAndIncludeNavigationType()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        var expectedId = 1;
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataForTestEntity());
        
        //act
        var actual = await dummyRepository.GetByIdAsync(expectedId, [x => x.TestEntityChildren]);
        
        //assert
        actual.ShouldNotBeNull();
        actual.Id.ShouldBe(expectedId);

        var testEntityChildren = actual.TestEntityChildren.ToList();
        testEntityChildren.ShouldNotBeEmpty();
        testEntityChildren[0].TestEntityId.ShouldBe(expectedId);
    }
    
    [Fact]
    internal async Task ShouldCorrectlyRetrieveAllItems()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataForTestEntity());
        
        //act
        var actual = await dummyRepository.ListAsync();
        
        //assert
        actual.ShouldNotBeNull();
        actual.ShouldNotBeEmpty();
    }
    
    [Fact]
    internal async Task ShouldCorrectlyRetrieveAllItemsWithPredicate()
    {
        //arrange
        var dummyRepository = new DummyRepository<TestEntity>(DbContextForTest);
        
        await SeedDataAsync(DummyRepositoryBaseTestsData.GetRangeOfSeedingDataForTestEntity());
        
        //act
        var actual = await dummyRepository.ListAsync(predicate => predicate.TestEntityChildren.Any(x => x.ChildName == "ChildFebruary")); //ChildFebruary
        
        //assert
        actual.ShouldNotBeNull();
        actual.ShouldNotBeEmpty();
    }
}
