using System;
using JADE_DOOR.Domain.Catalog;

namespace JADE_DOOR.Domain.Orders
{
    public class OrderItem
    {
        public int Id { get; set; }

        // Foreign key to the parent Order (if used by EF)
        public int OrderId { get; set; }

        // Reference to the catalog Item
    public int ItemId { get; set; }
    public Item? Item { get; set; }

        // Quantity of the item in this order
        public int Quantity { get; set; } = 1;

        // Unit price (read-only) taken from the referenced Item when available.
        // Keep this as the unit price so Order.TotalPrice can calculate: Price * Quantity
        public decimal Price => Item?.Price ?? 0m;
    }
}
