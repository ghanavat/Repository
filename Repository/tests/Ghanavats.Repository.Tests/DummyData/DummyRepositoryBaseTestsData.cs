namespace Ghanavats.Repository.Tests.DummyData;

internal static class DummyRepositoryBaseTestsData
{
    internal static List<TestEntity> GetRangeOfSeedingDataForTestEntity()
    {
        return
        [
            new TestEntity
            {
                TestProperty = "TestValueJanuary",
                TestEntityChildren = [
                    new TestEntityChild
                    {
                        ChildName = "ChildJanuary",
                        TestEntityId = 1
                    }
                ]
            },
            new TestEntity
            {
                TestProperty = "TestValueFebruary",
                TestEntityChildren = [
                    new TestEntityChild
                    {
                        ChildName = "ChildFebruary",
                        TestEntityId = 2
                    }
                ]
            },
            new TestEntity
            {
                TestProperty = "TestValueMarch",
                TestEntityChildren = [
                    new TestEntityChild
                    {
                        ChildName = "ChildMarch",
                        TestEntityId = 3
                    }
                ]
            },
            new TestEntity
            {
                TestProperty = "TestValueApril",
                TestEntityChildren = [
                    new TestEntityChild
                    {
                        ChildName = "ChildApril",
                        TestEntityId = 4
                    }
                ]
            },
            new TestEntity
            {
                TestProperty = "TestValueMay",
                TestEntityChildren = [
                    new TestEntityChild
                    {
                        ChildName = "ChildMay",
                        TestEntityId = 5
                    }
                ]
            }
        ];
    }
}
