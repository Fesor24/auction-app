using AuctionService.Dtos;
using AuctionService.Interfaces;
using AutoMapper;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Query.List;

public record GetAuctionsQuery : IRequest<Result<ICollection<GetAuctionDto>>>;

internal sealed class GetAuctionsQueryHandler : IRequestHandler<GetAuctionsQuery, Result<ICollection<GetAuctionDto>>>
{
    private readonly IAuctionRepository _auctionRepository;
    private readonly IMapper _mapper;

    public GetAuctionsQueryHandler(IAuctionRepository auctionRepository, IMapper mapper)
    {
        _auctionRepository = auctionRepository;
        _mapper = mapper;
    }

    public async Task<Result<ICollection<GetAuctionDto>>> Handle(GetAuctionsQuery request, CancellationToken cancellationToken)
    {
        return new Result<ICollection<GetAuctionDto>>(
            _mapper.Map<ICollection<GetAuctionDto>>(await _auctionRepository.GetAuctions()));
    }
}
