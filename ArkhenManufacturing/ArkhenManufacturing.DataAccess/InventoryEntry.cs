using System;

namespace ArkhenManufacturing.DataAccess
{
    public class InventoryEntry
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}
