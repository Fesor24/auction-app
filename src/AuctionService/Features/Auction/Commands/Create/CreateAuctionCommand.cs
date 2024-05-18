using AuctionService.Dtos;
using AuctionService.Interfaces;
using FluentValidation;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Commands.Create;

internal record CreateAuctionCommand(CreateAuctionDto Auction) : IRequest<Result<Guid>>;

internal class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator()
    {
        RuleFor(x => x.Auction.ImageUrl)
            .NotEmpty()
            .NotNull()
            .WithMessage("Image url can not be null or empty");

        RuleFor(x => x.Auction.ItemTitle)
            .NotEmpty()
            .NotNull()
            .WithMessage("Title can not be null or empty");
    }
}

internal sealed class CreateAuctionCommandHandler : IRequestHandler<CreateAuctionCommand, Result<Guid>>
{
    private readonly IAuctionRepository _auctionRepository;

    public CreateAuctionCommandHandler(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public async Task<Result<Guid>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        Domain.Auction auction = new()
        {
            ReservePrice = request.Auction.ReservePrice,
            Seller = "Test", // will be gotten from context later
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
        };

        await _auctionRepository.AddAsync(auction);

        await _auctionRepository.UnitOfWork.CompleteAsync();

        return auction.Id;
    }
}
