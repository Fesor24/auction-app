using AuctionService.Enums;

namespace AuctionService.Dtos;

public class CreateAuctionDto
{
    public decimal ReservePrice { get; set; }
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; }
    public string ItemTitle { get; set; }
    public string ItemArtist { get; set; }
    public decimal Height { get; set; }
    public decimal Width { get; set; }  
    public decimal Depth { get; set; }
    public Style Style { get; set; }
    public Medium Medium { get; set; }
    public string CurrentLocation { get; set; }
    public decimal Value { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAuthenticated { get; set; }
    public DateTime ItemCreatedAt { get; set; }
}
