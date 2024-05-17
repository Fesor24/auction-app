using AuctionService.Dtos;
using AuctionService.Features.Auction.Commands.Create;
using AuctionService.Features.Auction.Query.List;
using MediatR;

namespace AuctionService.Endpoints;

public static class AuctionEndpoints
{
    public static void Register(this WebApplication app)
    {
        const string ENDPOINT = "api/auction";

        app.MapPost(ENDPOINT, async (ISender sender, CreateAuctionDto auction) =>
        {
            var result = await sender.Send(new CreateAuctionCommand(auction));

            return result.Match
                (val => Results.CreatedAtRoute(), err => Results.BadRequest(err));
        });

        app.MapGet(ENDPOINT + "/list", async (ISender sender) =>
        {
            var result = await sender.Send(new GetAuctionsQuery());

            return result.Match(val => Results.Ok(val), err => Results.BadRequest(err));
        });
    }
}
