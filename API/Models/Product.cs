namespace API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Brand { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public long Price { get; set; }
        public int QuantityInStock { get; set; }
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
    }
}