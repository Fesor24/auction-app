using AuctionService.Domain;
using AuctionService.Dtos;
using AutoMapper;

namespace AuctionService.Mappings;

public class ItemMapping : Profile
{
    public ItemMapping()
    {
        CreateMap<Item, GetAuctionItemDto>()
            .ForMember(d => d.Height, f => f.MapFrom(s => s.Dimension.Height))
            .ForMember(d => d.Depth, f => f.MapFrom(s => s.Dimension.Depth))
            .ForMember(d => d.Width, f => f.MapFrom(s => s.Dimension.Width));
    }
}
