using AuctionService.Features.Auction.Commands.Create;
using MediatR;

namespace AuctionService.Endpoints;

public static class AuctionEndpoints
{
    public static void Register(this WebApplication app)
    {
        const string ENDPOINT = "api/auction";

        app.MapPost(ENDPOINT, async (ISender sender) =>
        {
            var result = await sender.Send(new CreateAuctionCommand());

            return result.Match
                (val => Results.CreatedAtRoute(), err => Results.BadRequest(err));
        });
    }
}
