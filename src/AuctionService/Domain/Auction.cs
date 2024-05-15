using AuctionService.Enums;
using Shared.Entity;

namespace AuctionService.Domain;

public class Auction : BaseEntity
{
    public decimal ReservePrice { get; set; }
    public string Seller { get; set; } = string.Empty;
    public string Winner { get; set; } = string.Empty;
    public decimal? SoldAmount { get; set; }
    public decimal? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set;} = DateTime.UtcNow;
    public DateTime AuctionEnd { get; set; }
    public Status Status { get; set; }
    public Item Item { get; set; }
}
