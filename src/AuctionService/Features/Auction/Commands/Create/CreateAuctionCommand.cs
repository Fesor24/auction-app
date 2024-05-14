using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Commands.Create;

internal record CreateAuctionCommand() : IRequest<Result<bool>>;

internal sealed class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(true);
    }
}
