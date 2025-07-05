namespace Ghanavats.Repository.Tests.DummyData;

/// <summary>
/// A sneaky fake repository class to hide the abstraction of the Repository Base.
/// Only used to help with testing the Repository Base.
/// </summary>
/// <param name="context"></param>
/// <typeparam name="T"></typeparam>
public class DummyRepository<T>(DummyDbContext context) : RepositoryBase<T>(context) where T : class;
