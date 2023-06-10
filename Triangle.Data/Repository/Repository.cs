using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using Triangle.Data.Contexts;
using Triangle.Data.IRepositories;
using Triangle.Domain.Commons;

namespace Triangle.Data.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    private readonly AppDbContext dbContext;
    private readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet = dbContext.Set<TEntity>();
    }
    public async ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);
        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }

        return false;
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await this.dbSet.AddAsync(entity);
        return entry.Entity;
    }

    public async ValueTask SaveChangesAsync()
        => await dbContext.SaveChangesAsync();


    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null)
    {
        IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);
        return query;
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression)
        => await this.SelectAll(expression).FirstOrDefaultAsync(t => !t.IsDeleted);

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        => this.dbContext.Update(entity).Entity;
}
