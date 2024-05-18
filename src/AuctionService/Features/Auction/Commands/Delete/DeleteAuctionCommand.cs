using AuctionService.Interfaces;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Commands.Delete;

public record DeleteAuctionCommand(Guid AuctionId) : IRequest<Result<bool>>;

internal sealed class DeleteAuctionCommandHandler : IRequestHandler<DeleteAuctionCommand, Result<bool>>
{
    private readonly IAuctionRepository _auctionRepository;

    public DeleteAuctionCommandHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<Result<bool>> Handle(DeleteAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _auctionRepository.GetAsync(x => x.Id == request.AuctionId);

        if (auction is null)
            return Error.NotFound("AUCTION_NOTFOUND", "Auction not found");

        _auctionRepository.Delete(auction);

        return await _auctionRepository.UnitOfWork.CompleteAsync() > 0;
    }
}
