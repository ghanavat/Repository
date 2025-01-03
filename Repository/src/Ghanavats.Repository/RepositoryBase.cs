﻿using System.Linq.Expressions;
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
    public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        where TId : notnull
    {
        var result = await _dbContext.Set<T>().FindAsync([id], cancellationToken);
        return result;
    }

    /// <inheritdoc/>
    public virtual Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public virtual Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
