using AuctionService.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using System.Linq.Expressions;

namespace AuctionService.Repository;

public class GenericRepository<TEntity>(AuctionDbContext context) : 
    IGenericRepository<TEntity> where TEntity : class
{
    private readonly AuctionDbContext _context = context;

    public async Task AddAsync(TEntity entity) =>
        await _context.Set<TEntity>().AddAsync(entity);

    public void Delete(TEntity entity) =>
        _context.Set<TEntity>().Remove(entity);

    public async Task<ICollection<TEntity>> GetAllAsync() =>
        await _context.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> criteria) =>
        await _context.Set<TEntity>().FirstOrDefaultAsync(criteria);

    public void Update(TEntity entity)
    {
        _context.Attach(entity);

        _context.Entry(entity).State = EntityState.Modified;
    }
}
