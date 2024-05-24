using AuctionService.Data;
using AuctionService.Domain;
using AuctionService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace AuctionService.Repository;

public class AuctionRepository(AuctionDbContext context) : 
    GenericRepository<Auction>(context), IAuctionRepository
{
    private readonly AuctionDbContext _context = context;

    public IUnitOfWork UnitOfWork => _context;

    public async Task<ICollection<Auction>> GetAuctions() =>
        await _context.Auction
        .Include(x => x.Item)
        .OrderByDescending(x => x.CreatedAt)
        .ToListAsync();
}
