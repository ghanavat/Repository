namespace Ghanavats.Repository.Tests.DummyData;

internal static class DummyRepositoryBaseTestsData
{
    internal static List<TestEntity> GetRangeOfSeedingDataFor()
    {
        return
        [
            new TestEntity
            {
                TestProperty = "TestValueJanuary"
            },
            new TestEntity
            {
                TestProperty = "TestValueFebruary"
            },
            new TestEntity
            {
                TestProperty = "TestValueMarch"
            },
            new TestEntity
            {
                TestProperty = "TestValueApril"
            },
            new TestEntity
            {
                TestProperty = "TestValueMay"
            }
        ];
    }
}
