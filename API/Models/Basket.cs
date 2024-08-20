namespace API.Models
{
    public class Basket : BaseModel
    {
        public Guid BuyerId { get; set; }
        public List<BasketItem> Items { get; set; } = [];

        public void AddItem(Product product, int quantity)
        {
            if (Items!.All(item => item.ProductId != product.Id)) 
            {
                Items!.Add(new BasketItem{ BasketId = Id, Basket = this, ProductId = product.Id, Product = product, Quantity = quantity});
            }

            var existingItem = Items!.FirstOrDefault(item => item.ProductId == product.Id);

            if (existingItem != null) existingItem.Quantity += quantity;
        }

        public void RemoveItem(Guid productId, int quantity)
        {
            var item = Items!.FirstOrDefault(item => item.ProductId == productId);

            if (item == null) return;

            item.Quantity -= quantity;

            if (item.Quantity <= 0)
            {
                Items!.Remove(item);
            }
        }
    }
}