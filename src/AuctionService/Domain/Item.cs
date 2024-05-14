using AuctionService.Enums;
using Shared.Entity;

namespace AuctionService.Domain;

public class Item : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public Dimension Dimension { get; set; }
    public DateTime CreatedAt { get; set; }
    public Style Style { get; set; }
    public Medium Medium { get; set; }
    public string CurrentLocation { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; }
}

public record Dimension(decimal Height, decimal Width, decimal Depth);
