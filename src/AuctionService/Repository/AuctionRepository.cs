using AuctionService.Data;
using AuctionService.Domain;
using AuctionService.Interfaces;

namespace AuctionService.Repository;

public class AuctionRepository : GenericRepository<Auction>, IAuctionRepository
{
    private readonly AuctionDbContext _context;

    public AuctionRepository(AuctionDbContext context): base(context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync() => await _context.CompleteAsync();
}
