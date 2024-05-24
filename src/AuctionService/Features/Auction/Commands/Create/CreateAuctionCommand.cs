using AuctionService.Dtos;
using AuctionService.Interfaces;
using AutoMapper;
using EventBus.Messages;
using FluentValidation;
using MassTransit;
using MediatR;
using Shared.Response;

namespace AuctionService.Features.Auction.Commands.Create;

internal record CreateAuctionCommand(CreateAuctionDto Auction) : IRequest<Result<GetAuctionDto>>;

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

internal sealed class CreateAuctionCommandHandler(IAuctionRepository auctionRepository, 
    IPublishEndpoint publishEndpoint, IMapper mapper) : 
    IRequestHandler<CreateAuctionCommand, Result<GetAuctionDto>>
{
    private readonly IAuctionRepository _auctionRepository = auctionRepository;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<GetAuctionDto>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
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

        var auctionDto = _mapper.Map<GetAuctionDto>(auction);

        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(auctionDto));

        return auctionDto;
    }
}
