using System.Linq.Expressions;
using BackendTemplate.Data.Entities;
using BackendTemplate.Database;
using BackendTemplate.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BackendTemplate.Data.Repositories;

public class RepositoryBase<TEntity> : IDisposable where TEntity : EntityBase
{
    protected readonly CadDbContext Context;
    protected readonly DbSet<TEntity> DbSet;

    public RepositoryBase(CadDbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }

    public virtual IEnumerable<TEntity> Find(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
    {
        IQueryable<TEntity> query = DbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return orderBy != null ? orderBy(query) : query;
    }

    public virtual TEntity FindById(object id)
    {
        return DbSet.Find(id) ?? throw new EntityNotFoundException();
    }
    
    public virtual async Task<TEntity> FindByIdAsync(object id)
    {
        return await DbSet.FindAsync(id) ?? throw new EntityNotFoundException();
    }

    public virtual void Create(TEntity entity)
    {
        DbSet.Add(entity);
    }
    
    public virtual async Task CreateAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public virtual void Delete(object id)
    {
        var entityToDelete = FindById(id);
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (Context.Entry(entityToDelete).State == EntityState.Detached)
        {
            DbSet.Attach(entityToDelete);
        }
        DbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        entityToUpdate.UpdatedAt = DateTime.UtcNow;
        DbSet.Attach(entityToUpdate);
        Context.Entry(entityToUpdate).State = EntityState.Modified;
    }
    
    private bool disposed;
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                Context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}