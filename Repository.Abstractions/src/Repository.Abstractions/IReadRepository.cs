using System.Linq.Expressions;

namespace Ghanavats.Repository.Abstractions;

/// <summary>
/// A base abstraction for read-only operations.
/// Don't use this interface directly for custom repositories.
/// </summary>
/// <typeparam name="T">An entity to which the repository operations will be written against</typeparam>
public interface IReadRepository<T> where T : class
{
    /// <summary>
    /// Finds an entity with the given id (typically is a primary key).
    /// </summary>
    /// <typeparam name="TId">The type of id.</typeparam>
    /// <param name="id">The value of the id.</param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="T" />, or <see langword="null"/>.
    /// </returns>
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

    /// <summary>
    /// Finds an entity with given id.
    /// It also includes related entities to the query
    /// </summary>
    /// <param name="id">The id that the query is matched against</param>
    /// <param name="includes">A list of related entities</param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="TId">The generic type of id.</typeparam>
    /// <returns></returns>
    Task<T?> GetByIdAsync<TId>(TId id, 
        Expression<Func<T, object>>[] includes, 
        CancellationToken cancellationToken = default)
        where TId : notnull;

    /// <summary>
    /// Finds all entities of <typeparamref name="T" /> from the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="T" />, that matches the predicate
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
}
