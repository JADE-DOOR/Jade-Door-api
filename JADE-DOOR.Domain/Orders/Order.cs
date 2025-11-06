using System;
using System.Collections.Generic;
using System.Linq;

namespace JADE_DOOR.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }

        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public decimal TotalPrice => Items?.Sum(ii => ii.Price * ii.Quantity) ?? 0m;
    }
}
