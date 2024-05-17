using AuctionService.Domain;
using Shared.Interfaces;

namespace AuctionService.Interfaces;

public interface IAuctionRepository : IGenericRepository<Auction>
{
    IUnitOfWork UnitOfWork { get; set; }

    Task<ICollection<Auction>> GetAuctions();
}
