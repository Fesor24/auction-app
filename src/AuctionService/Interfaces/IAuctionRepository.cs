using AuctionService.Domain;
using Shared.Interfaces;

namespace AuctionService.Interfaces;

public interface IAuctionRepository : IGenericRepository<Auction>
{
    IUnitOfWork UnitOfWork { get; }

    Task<ICollection<Auction>> GetAuctions();
}
