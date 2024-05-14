using System.Linq.Expressions;

namespace Shared.Interfaces;

public interface IGenericRepository<TEntity>
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> criteria);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task AddAsync(TEntity entity);
}
