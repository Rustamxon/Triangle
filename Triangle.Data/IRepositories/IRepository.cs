using System.Linq.Expressions;
using Triangle.Domain.Commons;

namespace Triangle.Data.IRepositories;

public interface IRepository<TEntity> where TEntity : Auditable
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    ValueTask SaveChangesAsync();
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression);
}
