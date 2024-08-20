using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class BasketItem : BaseModel
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public required Product Product { get; set; }
        public Guid BasketId { get; set; }
        public required Basket Basket { get; set; }
    }
}