using AuctionService.Dtos;
using AuctionService.Interfaces;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Commands.Create;

internal record CreateAuctionCommand(CreateAuctionDto Auction) : IRequest<Result<bool>>;

internal sealed class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Result<bool>>
{
    private readonly IAuctionRepository _auctionRepository;

    public CreateAuctionCommandHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<Result<bool>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        await _auctionRepository.AddAsync(new Domain.Auction
        {
            ReservePrice = request.Auction.ReservePrice,
            Seller = request.Auction.Seller,
            AuctionEnd = request.Auction.AuctionEnd,
            Status = request.Auction.Status,
            Item = new()
            {
                Value = request.Auction.Value,
                Medium = request.Auction.Medium,
                Artist = request.Auction.ItemArtist,
                CreatedAt = request.Auction.ItemCreatedAt,
                ImageUrl = request.Auction.ImageUrl,
                Dimension = new(request.Auction.Height, request.Auction.Width, request.Auction.Depth),
                CurrentLocation = request.Auction.CurrentLocation,
                IsAuthenticated = request.Auction.IsAuthenticated,
                Style = request.Auction.Style,
                Title = request.Auction.ItemTitle
            }
        });

        return await _auctionRepository.UnitOfWork.CompleteAsync() > 0;
    }
}
