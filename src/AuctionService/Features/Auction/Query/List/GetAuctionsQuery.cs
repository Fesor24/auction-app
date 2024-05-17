using AuctionService.Domain;
using AuctionService.Interfaces;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Query.List;

public record GetAuctionsQuery : IRequest<Result<ICollection<Domain.Auction>>>;

internal sealed class GetAuctionsQueryHandler : IRequestHandler<GetAuctionsQuery, Result<ICollection<Domain.Auction>>>
{
    private readonly IAuctionRepository _auctionRepository;

    public GetAuctionsQueryHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<Result<ICollection<Domain.Auction>>> Handle(GetAuctionsQuery request, CancellationToken cancellationToken)
    {
        return new Result<ICollection<Domain.Auction>>(await _auctionRepository.GetAuctions());
    }
}
