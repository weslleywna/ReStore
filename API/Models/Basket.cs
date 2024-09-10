namespace API.Models
{
    public class Basket : BaseModel
    {
        public Basket()
        {
            
        }

        public Basket(Guid buyerId)
        {
            BuyerId = buyerId;
        }

        public Basket(Guid id, Guid buyerId)
        {
            Id = id;
            BuyerId = buyerId;
        }

        public Guid BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = [];

        public void AddItem(Product product, int quantity)
        {
            var existingItem = Items.FirstOrDefault(item => item.ProductId == product.Id);
            if (existingItem == null) Items.Add(new BasketItem { BasketId = Id, Basket = this, ProductId = product.Id, Product = product, Quantity = quantity });
            else existingItem.Quantity += quantity;
        }

        public void RemoveItem(Guid productId, int quantity)
        {
            var item = Items.FirstOrDefault(item => item.ProductId == productId);
            if (item == null) return;
            item.Quantity -= quantity;
        }
    }
}