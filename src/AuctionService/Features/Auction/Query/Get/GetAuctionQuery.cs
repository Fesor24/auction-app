using AuctionService.Dtos;
using AuctionService.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Query.Get;

public record GetAuctionQuery(Guid AuctionId) : IRequest<Result<GetAuctionDto>>;

internal sealed class GetAuctionQueryHandler : IRequestHandler<GetAuctionQuery, Result<GetAuctionDto>>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IMapper _mapper;

    public GetAuctionQueryHandler(IAuctionRepository auctionRepository, IMapper mapper)
    {
        _auctionRepository = auctionRepository;
        _mapper = mapper;
    }

    public async Task<Result<GetAuctionDto>> Handle(GetAuctionQuery request, CancellationToken cancellationToken)
    {
        var auctions = await _auctionRepository.GetAllAsync();

        var auction = await _auctionRepository.GetAsync(x => x.Id == request.AuctionId);

        if (auction is null)
            return Error.NotFound("AUCTION_NOTFOUND", "Auction not found");

        return _mapper.Map<GetAuctionDto>(auction);
    }
}
