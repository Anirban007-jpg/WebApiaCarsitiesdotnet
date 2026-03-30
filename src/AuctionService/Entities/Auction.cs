namespace AuctionService.Entities;

public class Auction
{
    public Guid Id { get; set; }
    public int ReservePrice { get; set; } = 0;
    public string Seller { get; set; }
    public string Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public DateTime? AuctionEnd { get; set; }
    public Status Status { get; set; }
    public Item Item { get; set; }
    public int StartPrice { get; set; } = 0;
    public int EndPrice { get; set; } = 0;
    public int CurrentPrice { get; set; } = 0;
    public int CurrentBid { get; set; } = 0;
    public int CurrentBidder { get; set; } = 0;
    public int CurrentBidderId { get; set; } = 0;
    public int CurrentBidderName { get; set; } = 0;
    public int CurrentBidderAvatar { get; set; } = 0;
    public int CurrentBidderAvatarUrl { get; set; } = 0;
    public int CurrentBidderAvatarAlt { get; set; } = 0;
    public int CurrentBidderAvatarTitle { get; set; } = 0;
    public int CurrentBidderAvatarWidth { get; set; } = 0;
    public int CurrentBidderAvatarHeight { get; set; } = 0;
    public int CurrentBidderAvatarAltText { get; set; } = 0;
    public int CurrentBidderAvatarTitleText { get; set; } = 0;
}