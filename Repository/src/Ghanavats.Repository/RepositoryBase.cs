using System.Linq.Expressions;
using Ghanavats.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Ghanavats.Repository;

/// <inheritdoc/>
public abstract class RepositoryBase<T> : IRepository<T> 
    where T : class
{
    private readonly DbContext _dbContext;

    /// <summary>
    /// Repository Base constructor
    /// </summary>
    /// <param name="dbContext">EF DB Context dependency</param>
    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        return await _dbContext.Set<T>().FindAsync([id], cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public virtual async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }
}
