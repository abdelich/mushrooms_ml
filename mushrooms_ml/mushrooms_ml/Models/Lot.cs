namespace mushrooms_ml.Models
{
    public class Lot
    {
        public int Id { get; set; }
        public int MushroomId { get; set; }
        public Mushroom Mushroom { get; set; }
        public int UserId { get; set; }
        public AppUser User { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
