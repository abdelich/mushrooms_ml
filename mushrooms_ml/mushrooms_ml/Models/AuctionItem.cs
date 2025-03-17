using System;

namespace mushrooms_ml.Models
{
    public class AuctionItem
    {
        public int Id { get; set; }
        public int MushroomId { get; set; }
        public Mushroom Mushroom { get; set; }
        public int SellerId { get; set; }
        public int? BuyerId { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal? BuyoutPrice { get; set; }
        public bool IsSold { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }
    }
}
