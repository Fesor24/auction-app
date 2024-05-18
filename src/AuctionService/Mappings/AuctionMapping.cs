using AuctionService.Domain;
using AuctionService.Dtos;
using AutoMapper;

namespace AuctionService.Mappings;

public class AuctionMapping : Profile
{
    public AuctionMapping()
    {
        CreateMap<Auction, GetAuctionDto>();
    }
}
