using AuctionService.Domain;
using AuctionService.Dtos;
using AutoMapper;
using EventBus.Messages;

namespace AuctionService.Mappings;

public class AuctionMapping : Profile
{
    public AuctionMapping()
    {
        CreateMap<Auction, GetAuctionDto>();

        CreateMap<GetAuctionDto, AuctionCreated>()
            .ForMember(d => d.ItemCreatedAt, c => c.MapFrom(s => s.Item.CreatedAt))
            .ForMember(d => d.Height, c => c.MapFrom(s => s.Item.Height))
            .ForMember(d => d.Width, c => c.MapFrom(s => s.Item.Width))
            .ForMember(d => d.Depth, c => c.MapFrom(s => s.Item.Depth))
            .ForMember(d => d.Title, c => c.MapFrom(s => s.Item.Title))
            .ForMember(d => d.Artist, c => c.MapFrom(s => s.Item.Artist))
            .ForMember(d => d.Style, c => c.MapFrom(s => s.Item.Style))
            .ForMember(d => d.Medium, c => c.MapFrom(s => s.Item.Medium))
            .ForMember(d => d.CurrentLocation, c => c.MapFrom(s => s.Item.CurrentLocation))
            .ForMember(d => d.Value, c => c.MapFrom(s => s.Item.Value))
            .ForMember(d => d.ImageUrl, c => c.MapFrom(s => s.Item.ImageUrl))
            .ForMember(d => d.IsAuthenticated, c => c.MapFrom(s => s.Item.IsAuthenticated));
    }
}
