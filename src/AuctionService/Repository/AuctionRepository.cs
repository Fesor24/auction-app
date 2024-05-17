using AuctionService.Data;
using AuctionService.Domain;
using AuctionService.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;

namespace AuctionService.Repository;

public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
{
    private readonly AuctionDbContext _context;

    public AuctionRepository(AuctionDbContext context): base(context)
    {
        _context = context;
        UnitOfWork = context;
    }

    public IUnitOfWork UnitOfWork { get; set; }

    public async Task<ICollection<Auction>> GetAuctions() =>
        await _context.Auction
        .Include(x => x.Item)
        .ToListAsync();
}
