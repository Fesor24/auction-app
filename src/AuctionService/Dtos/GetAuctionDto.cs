namespace AuctionService.Dtos;

public class GetAuctionDto
{
    public string Id { get; set; }
    public decimal ReservePrice { get; set; }
    public string Seller { get; set; }
    public string Winner { get; set; }
    public decimal SoldAmount { get; set; }
    public decimal CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime AuctionEnd { get; set; }
    public string Status { get; set; }
    public GetAuctionItemDto Item { get; set; }
}

public class GetAuctionItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public decimal Width { get; set; }
    public decimal Height { get; set; }
    public decimal Depth { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Style { get; set; }
    public string Medium { get; set; }
    public string CurrentLocation { get; set; }
    public decimal Value { get; set; }
    public string ImageUrl { get; set; }
    public bool IsAuthenticated { get; set; }
}
